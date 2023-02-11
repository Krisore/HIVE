using HIVE.Server.Data;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HIVE.Server.Services
{
    public class CurriculumService : ICurriculumService
    {
        private readonly DataContext _context;

        public CurriculumService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Curriculum>> GetCurriculumBySearch(string value)
        {
            var result = await _context.Curriculums.Where(c => c.Name.ToLower().Contains(value.ToLower())).ToListAsync();
            return result;
        }

        public async Task<Curriculum> GetCurriculumById(int value)
        {
            var result = await _context.Curriculums.FirstOrDefaultAsync(c => c.Id == value);
            return result;
        }
    }
}
