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

        public async Task<List<Reference>> GetReferences()
        {
            var result = await _context.References.ToListAsync();
            return result;
        }

        public async Task AddReferenceAsync(Reference reference)
        {
            var request = new Reference
            {
                Name = reference.Name
            };
            _context.References.Add(request);
           await _context.SaveChangesAsync();
        }

        public async Task EditReferencesAsync(int id, Reference reference)
        {
            var request = await _context.References.FirstOrDefaultAsync(r => r.Id == id);
            if (request != null)
            {
                request.Name = reference.Name;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReferenceAsync(int id)
        {
            var response = await _context.References.FirstOrDefaultAsync(r => r.Id == id);
            if (response != null)
            {
                _context.References.Remove(response);
            }
            await _context.SaveChangesAsync();
        }
    }
}
