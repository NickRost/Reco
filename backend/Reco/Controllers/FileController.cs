using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Reco.BLL.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile()
        {
            Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value);
            var token = value.ToString();
            return Ok(await _fileService.SaveVideo(token));
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(int id)
        {
            Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value);
            var token = value.ToString();
          
            if(await _fileService.CheckAccessToFile(token, id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> FinishLoadingFile(int id, string uri)
        {
            await _fileService.FinishLoadingFile(id, uri);
            return Ok();
        }
    }
}
