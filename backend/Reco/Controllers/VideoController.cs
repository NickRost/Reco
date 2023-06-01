using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Reco.BLL.Services;
using Reco.Shared.Dtos;
using Reco.Shared.Dtos.Reactions;
using Reco.Shared.Dtos.Video;
using Reco.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reco.API.Extensions;

namespace Reco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly VideoService _videoService;
        private readonly ReactionService _reactionService;
        public VideoController(VideoService videoService, UserService userService, ReactionService reactionService)
        {
            _reactionService = reactionService;
            _videoService = videoService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetVideoById(int id)
        {
            return Ok(await _videoService.GetVideoById(id));
        }

        [HttpGet("folder/{id:int}")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideoByFolderId(int id)
        {
            return Ok(await _videoService.GetVideosByFolderId(id));
        }

        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideosByUserIdWithoutFolder(int id)
        {
            return Ok(await _videoService.GetVideosByUserIdWithoutFolder(id));
        }

        [HttpGet("check/{id:int}")]
        public async Task<ActionResult> GetFileState(int id)
        {
            return Ok(await _videoService.CheckVideoState(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value);
            var token = value.ToString();

            await _videoService.Delete(id, token);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateVideoDTO videoDTO)
        {
            await _videoService.Update(videoDTO);
            return NoContent();
        }

        [HttpPost("share")]
        public async Task<IActionResult> ShareVideo(VideoShareDTO sharePostInfo)
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());
            await _videoService.SendEmail(sharePostInfo.Email, sharePostInfo.Link, user.WorkspaceName);
            return Ok();
        }

        
        [HttpPost("react")]
        public async Task<IActionResult> ReactVideo(NewVideoReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();
            await _reactionService.ReactVideo(reaction);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<VideoDTO>> CreateVideo(NewVideoDTO newVideo)
        {
            var createdVideo = await _videoService.AddVideo(newVideo);
            return Ok(createdVideo);
        }
    }
}
