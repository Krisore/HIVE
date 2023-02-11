using HIVE.Server.Data;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthorController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthorAsync()
        {
            var response = await _context.Authors.ToListAsync();
            return Ok(response);
        }
        [HttpPost]
        public async Task AddAuthor(CreateAuthor author)
        {
            if (author.Name != null)
            {
                var newAuthor = new Author
                {
                    Name = author.Name,
                    DocumentId = author.DocumentId,
               
                };
                _context.Authors.Add(newAuthor);
            }
            await _context.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task DeleteAuthor(int id)
        {
            try
            {
                var result = await _context.Authors.FindAsync(id);
                if (result != null) _context.Authors.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
