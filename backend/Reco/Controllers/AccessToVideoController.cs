using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reco.BLL.Services;
using Reco.Shared.Dtos.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reco.API.Controllers
{
    [Route("api/access")]
    [ApiController]
    public class AccessToVideoController : ControllerBase
    {
        private readonly AccessToVideoService _accessToVideoService;
        public AccessToVideoController(AccessToVideoService accessToVideoService)
        {
            _accessToVideoService = accessToVideoService;
        }

        [HttpPut]
        public async Task<IActionResult> AddNewAccess(AccessForUnregisteredUsersDTO accessForUnregisteredUsersDTO)
        {   
            await _accessToVideoService.AddUserAccess(accessForUnregisteredUsersDTO.Email, accessForUnregisteredUsersDTO.VideoId);
            return Ok();
        }
        
        [HttpGet("check")]
        public async Task<ActionResult<bool>> CheckRegisteredUser(int videoId, int userId)
        {
            return Ok (await _accessToVideoService.CheckRegisteredUser(videoId, userId));
        }
    }
}
