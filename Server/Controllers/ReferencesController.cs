using HIVE.Server.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIVE.Shared.Model;
namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly IReferenceService _referenceService;

        public ReferencesController(IReferenceService referenceService )
        {
            _referenceService = referenceService;
        }

        [HttpGet("{value}")]
        public async Task<ActionResult<IEnumerable<Reference>>> GetReferenceBySearchAsync(string value)
        {
            var result = await _referenceService.GetReferenceBySearch(value);
            return Ok(result);

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Reference>>> GetReferenceById(int id)
        {
            var result = await _referenceService.GetReference(id);
            return Ok(result);

        }
    }
}
