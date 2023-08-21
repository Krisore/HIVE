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
        private readonly IAzureStorageHelper _azureStorageHelper;
        private readonly string _containerName;

        public FileController(IAzureStorageHelper azureStorageHelper)
        {
            _azureStorageHelper = azureStorageHelper;
            _containerName = "puphive";
        }
        [HttpPost]
        public async Task<ActionResult<FileEntry>> UploadFileAsync(List<IFormFile> files)
        {
            var response = await _azureStorageHelper.UploadFileAsync(files, _containerName, true);
            return Ok(response);
        }
        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFileAsync(string fileName)
        {
            var uploadResult = await _azureStorageHelper.DownloadFileAsync(fileName);
            var container = _azureStorageHelper.OpenContainer(_containerName);
            var blob = container.GetBlobClient(fileName);
            var stream = await blob.DownloadAsync();
            return File(stream.Value.Content, uploadResult.ContentType, fileName);
        }
        [HttpGet]
        public async Task<ActionResult<List<FileEntry>>> GetFilesAsync()
        {
            var files = await _azureStorageHelper.GetFiles();
            return Ok(files);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFileAsync(int id)
        {
            await _azureStorageHelper.DeleteFileAsync(id, _containerName);
            return Ok();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FileEntry>> GetFileById(int id)
        {
            var file = await _azureStorageHelper.GetFileId(id);
            return Ok(file);
        }
    }
}
