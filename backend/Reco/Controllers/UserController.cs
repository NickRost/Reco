using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reco.API.Extensions;
using Reco.BLL.Services;
using Reco.Shared.Dtos.User;
using System.Threading.Tasks;

namespace Reco.API.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly TeamService _teamService;

        public UserController(AuthService authService, UserService userService, TeamService teamService)
        {
            _authService = authService;
            _userService = userService;
            _teamService = teamService;
        }

        [HttpPost("Reset-Password/{email}")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            await _userService.ResetPassword(email);
            return NoContent();
        }

        [HttpPost("Reset-Password-Finish/{email}/{newPass}")]
        public async Task<IActionResult> ResetPasswordDone(string email, string newPass)
        {
            var loginDto = await _userService.ResetPasswordFinish(email, newPass);
            var auth = await _authService.Authorize(loginDto, false);

            return Ok(auth);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromForm] UpdateUserDTO userDTO, [FromForm] IFormFile avatar)
        {
            await _userService.UpdateUser(userDTO, avatar);
            return NoContent();
        }

        [HttpPost("Update-Password-Email")]
        public async Task<IActionResult> UpdatePasswordEmail([FromBody] UpdateUserDTO userDTO)
        {
            await _userService.UpdateUserPasswordEmail(userDTO);
            return NoContent();
        }

        [HttpPost("Reset-Password/{userId:int}")]
        public async Task<IActionResult> ResetPassword(int userId)
        {
            await _userService.ResetPassword(userId);
            return NoContent();
        }

        [HttpPost("Delete-User/{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpGet]
        [Route("FromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());

            return Ok(user);
        }

        [HttpPost]
        [Route("Add-To-Team/{token}")]
        public async Task<IActionResult> AddToTeam(string token)
        {
            await _userService.AddToTeam(this.GetUserIdFromToken(), token);

            return NoContent();
        }

        [HttpGet]
        [Route("Send-Invite-Link/{email}")]
        public async Task<IActionResult> SendInviteLink(string email)
        {
            await _teamService.SendInviteLink(this.GetUserIdFromToken(), email);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }
    }
}