using HIVE.Server.Data;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Services
{
    public class DocumentHistoryService : IDocumentHistoryService
    {
        private readonly DataContext _context;

        public DocumentHistoryService(DataContext context)
        {
            _context = context;
        }
        public async Task<DocumentHistory> InsertEditLog(DocumentHistory modified)
        {
          
            var history = new DocumentHistory
            {
                Title = modified.Title,
                ModifiedDate = modified.ModifiedDate,
                DocumentId = modified.DocumentId,
                Owner = modified.Owner,
            };
            _context.DocumentHistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<List<DocumentHistory>> GetDocumentHistoriesById(int id)
        {
            var result = await _context.DocumentHistories.Where(d => d.DocumentId == id).ToListAsync();
            return result;
        }
    }
}
