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
        [HttpGet]
        public async Task<ActionResult<List<Reference>>> GetReferences()
        {
            var result = await _referenceService.GetReferences();
            return Ok(result);

        }
        [HttpPost]
        public async Task<ActionResult> AddReferenceAsync(Reference reference)
        { 
            await _referenceService.AddReferenceAsync(reference);
            return Ok();

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateReferenceAsync(int id, Reference reference)
        {
            await _referenceService.EditReferencesAsync(id, reference);
            return Ok();

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteReferenceAsync(int id)
        {
            await _referenceService.DeleteReferenceAsync(id);
            return Ok();
        }
    }
}
