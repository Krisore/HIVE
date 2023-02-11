using HIVE.Server.Repository;
using HIVE.Server.Repository.Interface;
using HIVE.Shared.Model;
using HIVE.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _repository;

        public DocumentController(IDocumentRepository repository)
        {
            _repository = repository;
        }
        public Document Document { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();
        [HttpGet]
        public async Task<ActionResult<List<Document>>> GetDocumentsAsync()
        {
            Documents = await _repository.GetDocumentsAsync();
            return Ok(Documents);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Document>> GetDocumentAsyncById(int id)
        {
            var result = await _repository.GetDocumentAsyncById(id);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _repository.DeleteDocumentAsync(id);
            return Ok();
        }
        [HttpGet]
        [Route("{owner}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<Document>>> GetMyDocuments(string owner)
        {
            var result = await _repository.GetMyDocumentsAsync(owner);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UploadDocumentAsync(UploadDocumentRequest request)
        {
            try
            {
                Document = await _repository.AddDocumentAsync(request);
                return Ok(Document);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return NotFound();
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult<Document>> UpdateDocumentAsync(int id, UploadDocumentRequest document)
        {
            try
            {
                var result = await _repository.EditDocumentAsync(id, document);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return NotFound();
        }
        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<ActionResult<UserRegisterRequest>> GetDocumentByDataTransferAsync(int id)
        {
            var response = await _repository.GetDocumentDataTransferAsync(id);
            if (response == null) throw new ArgumentNullException(nameof(response));
            return Ok(response);
        }
    }
}
