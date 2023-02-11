using System.Collections;
using HIVE.Server.Data;
using HIVE.Shared.Model;
using HIVE.Server.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace HIVE.Server.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly DataContext _context;

        public ReferenceService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable> GetReferenceBySearch(string search)
        {
            var result = await _context.References.Where(dt => dt.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            return result;
        }
        public async Task<Reference> GetReference(int value)
        {
            var result = await _context.References.FirstOrDefaultAsync(d => d.Id == value);
            return result;
        }
    }
}
