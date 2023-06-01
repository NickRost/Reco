using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Reco.Blob.BLL.Services;
using Reco.BlobAPI.Dtos;

namespace Reco.BlobAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly FileService _blobService;

        public FileController(FileService blobService)
        {
            _blobService = blobService;
        }


        [HttpGet]
        public async Task<IActionResult> GetFile(int id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var (response, errorCode) = await _blobService.DownloadAsync(id, accessToken);
            if (errorCode.HasValue)
                return Unauthorized();
            return Ok(File(response, "application/mp4", id.ToString() + ".mp4"));
        }

        [HttpGet("GetUrl")]
        public async Task<ActionResult<FileDto>> GetFileUrl(int id)
        {
            var token = Request.Headers[HeaderNames.Authorization];

            var response = await _blobService.GetUrlAsync(id, token.ToString());

            return Ok(new FileDto(response));
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        public async Task<IActionResult> UploadFile()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            int videoId = 0;
            try
            {
                var headerResult = Request.Headers["videoId"].First().ToString();
                videoId = Convert.ToInt32(headerResult);
            }
            catch
            {
                return BadRequest();
            }

            Stream file = Request.Body;
            var responseId = await _blobService.UploadAsync(file, accessToken, videoId);
            if (responseId != null)
            {
                return Ok(responseId);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var token = Request.Headers[HeaderNames.Authorization];

            bool result = await _blobService.DeleteAsync(id, token);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}