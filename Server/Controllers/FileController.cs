using HIVE.Server.Repository.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        [HttpPost]
        public async Task<ActionResult<FileEntry>> UploadFileAsync(List<IFormFile> files)
        {
            var response = await _fileManager.UploadFileAsync(files);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<List<FileEntry>>> GetFilesAsync()
        {
            var files = await _fileManager.GetFiles();
            return Ok(files);
        }
    }
}
