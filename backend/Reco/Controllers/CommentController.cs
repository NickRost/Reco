using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reco.API.Extensions;
using Reco.BLL.Services;
using Reco.Shared.Dtos.Comment;
using Reco.Shared.Dtos.Reactions;
using Reco.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController: ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly ReactionService _reactionService;
        public CommentController(CommentService commentService, ReactionService reactionService)
        {
            _commentService = commentService;
            _reactionService = reactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> GetComments(int videoId)
        {
            return Ok(await _commentService.GetAllVideosComments(videoId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComments(CommentDTO comment)
        {
            await _commentService.UpdateComment(comment);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateComment(NewCommentDTO comment)
        {
            await _commentService.CreateComment(comment);
            var allComments = await _commentService.GetAllVideosComments(comment.VideoId);
            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteComment(id);
            return NoContent();
        }

        [HttpPost("react")]
        public async Task<IActionResult> ReactComment(NewCommentReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();
            await _reactionService.ReactComment(reaction);

            return Ok();
        }

        [HttpDelete("react")]
        public async Task<IActionResult> DeleteReaction(int commentId, Reaction reaction)
        {
            var newReaction = new NewCommentReactionDTO {
                CommentId = commentId,
                Reaction = reaction
            };
            newReaction.UserId = this.GetUserIdFromToken();
            await _reactionService.ReactComment(newReaction);
            return NoContent();
        }
    }
}