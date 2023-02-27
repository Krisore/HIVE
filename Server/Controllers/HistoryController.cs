using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IDocumentHistoryService _historyService;

        public HistoryController(IDocumentHistoryService historyService)
        {
            _historyService = historyService;
        }
        [HttpPost("write")]
        public async Task<ActionResult<DocumentHistory>> InsertModifiedLog(DocumentHistory modified)
        {
            var result = await _historyService.InsertEditLog(modified);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<DocumentHistory>>> GetDocumentHistories(int id)
        {
            var histories = await _historyService.GetDocumentHistoriesById(id);
            return Ok(histories);
        }
    }
}
