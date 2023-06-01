using Microsoft.AspNetCore.Mvc;
using Reco.BLL.Services;
using Reco.Shared.Dtos.Folder;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reco.API.Controllers
{
    [ApiController]
    [Route("api/folders")]
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;

        public FolderController(FolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpPost]
        public async Task<ActionResult<FolderDTO>> Create([FromBody] NewFolderDTO newFolderDTO)
        {
            return Ok(await _folderService.Create(newFolderDTO));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<FolderDTO>>> Get(int id)
        {
            return Ok(await _folderService.GetFoldersByAuthorId(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _folderService.Delete(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] FolderDTO folderDto)
        {
            await _folderService.Update(folderDto);
            return NoContent();
        }
    }
}
