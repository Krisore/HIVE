using HIVE.Server.Data;
using HIVE.Server.Repository.Interface;
using HIVE.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;

        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetDocumentsAsync()
        {
            var result = await _context.Documents
                .Where(d => d.IsActive && d.ToReview == false && d.IsArchived == false && d.IsOpenAccess)
                .Include(d => d.Curriculum)
                .Include(a => a.Adviser).Include(r => r.Reference).Include(t => t.Topics).ToListAsync();
            return result;
        }
        public async Task<List<Document>> GetMyDocumentsAsync(string owner)
        {
            var result = await _context.Documents
                .Where(d => d.UploaderEmail == owner)
                .Include(c => c.Curriculum)
                .Include(adviser => adviser.Adviser)
                .Include(t => t.Topics)
                .Include(r => r.Reference).ToListAsync();
            return result;
        }
    }
}
