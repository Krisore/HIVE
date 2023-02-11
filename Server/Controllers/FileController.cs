using Azure.Storage.Blobs;
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
        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFileAsync(string fileName)
        {
            var uploadResult = await _fileManager.DownloadFileAsync(fileName);
            var container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=puparch;AccountKey=ggiTXy86V3PzvZoDLvjSM9EiKIViz0WG1tPWxh16YTSg4NP2TqQBqMF+2/LUSKw/wnuW53rgsqEU+ASt5LmhUQ==;BlobEndpoint=https://puparch.blob.core.windows.net/;TableEndpoint=https://puparch.table.core.windows.net/;QueueEndpoint=https://puparch.queue.core.windows.net/;FileEndpoint=https://puparch.file.core.windows.net/", "pupstored");
            var blob = container.GetBlobClient(fileName);
            var stream = await blob.DownloadAsync();
            return File(stream.Value.Content, uploadResult.ContentType, fileName);
        }
        [HttpGet]
        public async Task<ActionResult<List<FileEntry>>> GetFilesAsync()
        {
            var files = await _fileManager.GetFiles();
            return Ok(files);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFileAsync(int id)
        {
            await _fileManager.DeleteFileAsync(id);
            return Ok();
        }
    }
}
