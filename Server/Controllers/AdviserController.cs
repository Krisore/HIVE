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

    }
}
