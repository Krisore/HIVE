using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdviserController : ControllerBase
    {
        private readonly IAdviserService _adviserService;

        public AdviserController(IAdviserService adviserService)
        {
            _adviserService = adviserService;
        }

        [HttpGet("{value}")]
        public async Task<ActionResult<IEnumerable<Adviser>>> GetAdviserByAsyncSearch(string value)
        {
            var result = await _adviserService.GetAdviserBySearchAsync(value);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Adviser>> GetAdviserByAsyncSearch(int id)
        {
            var result = await _adviserService.GetAdviserAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<Adviser>>> GetAllAdviserAsync()
        {
            var advisers = await _adviserService.GetAllAdviserAsync();
            return Ok(advisers);
        }
        //TODO: Functions that ADD Adviser in Archivist / Student Done ✔️
        [HttpPost]
        public async Task<ActionResult<Adviser>> AddAdviserAsync(Adviser adviser)
        {
            var result = await _adviserService.AddAdviserAsync(adviser);
            return Ok(adviser);

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditAdviserAsync(int id, Adviser adviser)
        {
           await _adviserService.EditAdviserAsync(id, adviser);
            return Ok();
        }
        //TODO: Functions that DELETE the Adviser || Done ✔️
        [HttpDelete("{adviserId:int}")]
        public async Task<ActionResult> DeleteAdviserAsync(int adviserId)
        {
           await _adviserService.DeleteAdviserAsync(adviserId);
            return Ok();
        }

    }
}
