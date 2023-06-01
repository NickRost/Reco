using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reco.BLL.Services.Abstract;
using Reco.DAL.Entities;
using Reco.Shared.Dtos.Auth;
using Reco.Shared.Dtos.User;
using Reco.BLL.Exceptions;
using Reco.BLL.JWT;
using Reco.Shared.Security;
using Reco.DAL.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reco.BLL.Services
{
    public class UserService : BaseService
    {
        private readonly TeamService _teamService;
        private readonly EmailService _emailService;
        private readonly ImageService _imageService;
        private readonly IConfiguration _configuration;
        private readonly JwtFactory _jwtFactory;
        private readonly AuthService _authService;

        public UserService(RecoDbContext context, IMapper mapper, AuthService authService,
            IConfiguration configuration, ImageService imageService, JwtFactory jwtFactory,
            TeamService teamService, EmailService emailService)
            : base(context, mapper)
        {
            _authService = authService;
            _configuration = configuration;
            _teamService = teamService;
            _emailService = emailService;
            _jwtFactory = jwtFactory;
            _imageService = imageService;
        }

        public async Task<UserDTO> CreateUser(NewUserDTO userRegisterDTO)
        {
            //var userEntity = _mapper.Map<User>(userRegisterDTO);
            var userEntity = new User()
            {
                AvatarLink = userRegisterDTO.AvatarLink,
                CreatedAt = DateTime.Now,
                Email = userRegisterDTO.Email,
                Id = userRegisterDTO.Id,
                IsEmailConfirmed = false,
                Password = userRegisterDTO.Password,
                Permissions = new List<Permission>(),
                WorkspaceName = userRegisterDTO.WorkspaceName,
                Teams = new List<Team>()
            };
            userEntity.CreatedAt = DateTime.UtcNow;

            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(userRegisterDTO.Password, salt);

            var existUser = _context.Users.FirstOrDefault(u => u.Email == userRegisterDTO.Email);
            if (existUser != null)
            {
                throw new ExistUserException(userRegisterDTO.Email);
            }

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            await _teamService.CreateTeam(userEntity);
            userEntity = _context.Users.Include(q => q.Teams)
                .FirstOrDefault(u => u.Email == userRegisterDTO.Email);

            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task UpdateUser(UpdateUserDTO userDto, IFormFile avatar)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (userEntity == null) { return; }

            if (avatar != null)
            {
                userEntity.AvatarLink = await _imageService.UploadToGyazo(avatar, _configuration["GyazoKey"]);
            }

            if (string.IsNullOrEmpty(userEntity.WorkspaceName) == false)
            {
                userEntity.WorkspaceName = userDto.WorkspaceName;
                await _teamService.UpdateTeam(userDto.Id, userDto.WorkspaceName);
            }

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPasswordEmail(UpdateUserDTO userDto)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (userEntity == null) { return; }

            if (!SecurityHelper.IsValidPassword(userEntity.Password, userDto.PasswordCurrent, userEntity.Salt))
            {
                throw new InvalidUserNameOrPasswordException();
            }

            userEntity.Email = userDto.Email ?? userEntity.Email;
            if (!string.IsNullOrWhiteSpace(userDto.PasswordNew) &&
                !string.IsNullOrWhiteSpace(userDto.PasswordCurrent))
            {
                var salt = SecurityHelper.GetRandomBytes();
                userEntity.Salt = Convert.ToBase64String(salt);
                userEntity.Password = SecurityHelper.HashPassword(userDto.PasswordNew, salt);
            }

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPassword(int userId)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userEntity == null) { return; }

            string newPassword = Guid.NewGuid().ToString().Substring(0, 10);
            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(newPassword, salt);

            string message = "Temp password: " + newPassword;
            await _emailService.SendEmailAsync(userEntity.Email, "New Password", message);
        }

        public async Task DeleteUser(int userId)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userEntity == null) { return; }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Users)
                .FirstOrDefaultAsync(p => p.Id == userId);

            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task AddToTeam(int userId, string token)
        {
            string authorId = _jwtFactory.GetValueFromToken(token, "sub");
            int authorIdInt = Convert.ToInt32(authorId);

            var author = await _context.Users.Where(q => q.Id == authorIdInt).FirstOrDefaultAsync();
            if (author == null)
            {
                throw new NotFoundException("User");
            }

            var team = await _context.Teams.Include(q => q.Users).Where(q => q.AuthorId == author.Id).FirstOrDefaultAsync();
            if (team == null)
            {
                throw new NotFoundException("Team");
            }

            var user = await _context.Users.Where(q => q.Id == userId).FirstOrDefaultAsync();
            team.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> CreateGoogleUser(ExternalAuthDto userRegisterDTO,
            GoogleJsonWebSignature.Payload payload)
        {
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == payload.Email);

            if (userEntity == null)
            {
                var newUserDTO = new NewUserDTO
                {
                    Email = payload.Email,
                    Password = userRegisterDTO.IdToken,
                    WorkspaceName = userRegisterDTO.WorkspaceName
                };

                return await CreateUser(newUserDTO);
            }
            else
            {
                return _mapper.Map<UserDTO>(userEntity);
            }
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            return _mapper.Map<UserDTO>(user);
        }
        public async Task ResetPassword(string email)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existUser == null)
            {
                throw new UserNotFoundException(email);
            }

            var token = await _authService.GenerateAccessToken(existUser.Id, existUser.Email, existUser.Email);
            string accessToken = token.AccessToken;
            string clientHost = _configuration["ClientHost"];
            string url = $"{clientHost}/reset-finish?token={accessToken}";

            await _emailService.SendEmailAsync(email, "New Password", url);
        }

        public async Task<LoginUserDTO> ResetPasswordFinish(string email, string newPass)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existUser == null)
            {
                throw new UserNotFoundException(email);
            }

            var salt = SecurityHelper.GetRandomBytes();
            existUser.Salt = Convert.ToBase64String(salt);
            existUser.Password = SecurityHelper.HashPassword(newPass, salt);

            _context.Users.Update(existUser);
            await _context.SaveChangesAsync();

            return new LoginUserDTO { Email = existUser.Email, Password = newPass };
        }
    }
}
