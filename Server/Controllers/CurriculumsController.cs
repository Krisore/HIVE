using HIVE.Server.Data;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculumsController : ControllerBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly DataContext _context;

        public CurriculumsController(ICurriculumService curriculumService, DataContext context)
        {
            _curriculumService = curriculumService;
            _context = context;
        }

        [HttpGet("{value}")]
        public async Task<ActionResult<IEnumerable<Curriculum>>> GetCurriculumAsyncBySearch(string value)
        {
            var result = await _curriculumService.GetCurriculumBySearch(value);
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Curriculum>> GetCurriculumById(int id)
        {
            var result = await _curriculumService.GetCurriculumById(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<Curriculum>>> GetAllAcademicPrograms()
        {
            var academics = await _context.Curriculums.ToListAsync();
            return Ok(academics);
        }
        [HttpPost("create")]
        public async Task<ActionResult> AddAcademicProgramAsync(Curriculum curriculum)
        {
            var newProgram = new Curriculum()
            {
                Name = curriculum.Name,
                Alt = curriculum.Alt
            };
            _context.Curriculums.Add(newProgram);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAcademicProgramAsync(int id, Curriculum program)
        {
            var result = await _context.Curriculums
                .FirstOrDefaultAsync(a => a.Id == id);
            if (result != null)
            {
                result.Name = program.Name;
                result.Alt = program.Alt;
            }
            await _context.SaveChangesAsync();
            return Ok();

        }
        //TODO: Add a DELETE method in AcademicController | Done ✔️
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAcademicProgramAsync(int id)
        {
            var result = await _context.Curriculums.FirstOrDefaultAsync(ap => ap.Id == id);
            if (result != null) _context.Curriculums.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
