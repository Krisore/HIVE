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

        private Document Document { get; set; } = new();
        private List<Document> Documents { get; set; } = new();
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
            return Ok(response);
        }

        [HttpGet]
        [Route("archivist")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Document>>> GetDocumentsForArchivist()
        {
            try
            {
                var result = await _repository.GetDocumentsForArchivist();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }

        }
        [HttpGet]
        [Route("archivist/document/archived/{id:int}")]
        public async Task<ActionResult> ArchiveDocument(int id)
        {
            await _repository.ArchiveDocument(id);
            return Ok();
        }
        [HttpGet]
        [Route("archivist/document/trash/{id:int}")]
        public async Task<ActionResult> MoveToTrash(int id)
        {
            await _repository.MoveToTrashAsync(id);
            return Ok();
        }
        [HttpGet]
        [Route("archivist/document/delete/{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
        [HttpGet]
        [Route("archivist/document/unarchived/{id:int}")]
        public async Task<ActionResult> UnArchiveDocument(int id)
        {
            await _repository.UnArchiveDocument(id);
            return Ok();
        }
        [HttpGet]
        [Route("archivist/document/trash/restore/{id:int}")]
        public async Task<ActionResult> Restore(int id)
        {
            await _repository.RestoreDocument(id);
            return Ok();
        }
        [HttpGet]
        [Route("archivist/document/update/review/{id:int}")]
        public async Task<ActionResult> UpdateStatusAsync(int id)
        {
            await _repository.UpdateDocumentStatus(id);
            return Ok();
        }
        [HttpGet]
        [Route("archivist/document/archived/")]
        public async Task<ActionResult<List<Document>>> GetArchivedDocumentsAsync()
        {
            var response = await _repository.GetArchivedDocumentsAsync();
            return Ok(response);
        }
        [HttpGet]
        [Route("archivist/document/trash/")]
        public async Task<ActionResult<List<Document>>> GetDocumentsInTrashAsync()
        {
            var response = await _repository.GetTrashedDocumentsAsync();
            return Ok(response);
        }

    }
}
