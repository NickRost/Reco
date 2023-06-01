using Reco.BLL.Services.Abstract;
using Reco.DAL.Context;
using Reco.DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;
using Reco.Shared.Dtos.Access;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Reco.DAL.Entities;

namespace Reco.BLL.Services
{
    public class AccessToVideoService : BaseService
    {
        private UserService _userService;
        public AccessToVideoService(RecoDbContext context, IMapper mapper, UserService userService)
            : base(context, mapper)
        {
            _userService = userService;
        }

        public async Task AddUserAccess(string email, int videoId)
        {
            var unRegisteredUser = new AccessForUnregisteredUsersDTO
            {
                Email = email,
                VideoId = videoId
            };
            var user = await _userService.GetUserByEmail(email);
            var accessedUser = await FindUnregisteredUser(unRegisteredUser);
            if (accessedUser == null)
            {
                if (user != null)
                {
                    var registeredUser = new AccessForRegisteredUsers
                    {
                        UserId = user.Id,
                        VideoId = videoId
                    };
                    var foundRegisteredUser = await FindRegisteredUserAccess(_mapper.Map<AccessForRegisteredUsersDTO>(registeredUser));
                    if (foundRegisteredUser == null)
                    {
                        _context.AccessesForRegisteredUsers.Add(registeredUser);
                    }
                }
                else
                {
                    var unRegisteredUserEntity = _mapper.Map<AccessForUnregisteredUsers>(unRegisteredUser);
                    _context.AccessesForUnregisteredUsers.Add(unRegisteredUserEntity);
                }
            }
            else
            {
                await ChangeUserAccess(unRegisteredUser);
            }
            _context.SaveChanges();
        }

        public async Task ChangeUserAccess(AccessForUnregisteredUsersDTO unregisteredUser)
        {
            var foundUnregisteredUserDTO = await FindUnregisteredUser(unregisteredUser);
            var foundUnregisteredUser = _mapper.Map<AccessForUnregisteredUsers>(foundUnregisteredUserDTO);
            var foundUser = await _userService.GetUserByEmail(unregisteredUser.Email);
            var registeredUser = new AccessForRegisteredUsers
            {
                UserId = foundUser.Id,
                VideoId = unregisteredUser.VideoId
            };
            _context.AccessesForUnregisteredUsers.Remove(foundUnregisteredUser);
            _context.AccessesForRegisteredUsers.Add(registeredUser);
            _context.SaveChanges();
        }

        private async Task<AccessForUnregisteredUsersDTO> FindUnregisteredUser(AccessForUnregisteredUsersDTO unregisteredUser)
        {
            var accessedUser = await _context.AccessesForUnregisteredUsers.FirstOrDefaultAsync(
                user => user.Email == unregisteredUser.Email && user.Email == unregisteredUser.Email
            );
            return accessedUser == null ? null : _mapper.Map<AccessForUnregisteredUsersDTO>(accessedUser);
        }

        public async Task<AccessForRegisteredUsersDTO> FindRegisteredUserAccess(AccessForRegisteredUsersDTO accessedUserDTO)
        {
            var accessedUser = await _context.AccessesForRegisteredUsers.FirstOrDefaultAsync(
                user => user.UserId == accessedUserDTO.UserId && user.VideoId == accessedUserDTO.VideoId);
            return accessedUser == null ? null : _mapper.Map<AccessForRegisteredUsersDTO>(accessedUser);
        }

        public async Task<bool> CheckRegisteredUser(int videoId, int userId)
        {
            var accessedUsers = await _context.AccessesForRegisteredUsers.Where(user => user.UserId == userId).ToListAsync();
            var foundUser = accessedUsers.FirstOrDefault(user => user.VideoId == videoId);
            if (foundUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}