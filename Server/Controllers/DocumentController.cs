using HIVE.Server.Repository.Interface;
using HIVE.Shared.Model;
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

        [HttpGet]
        public async Task<ActionResult<List<Document>>> GetDocumentsAsync()
        {
            var result = await _repository.GetDocumentsAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("{owner}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<Document>>> GetMyDocuments(string owner)
        {
            var result = await _repository.GetMyDocumentsAsync(owner);
            return Ok(result);
        }
    }
}
