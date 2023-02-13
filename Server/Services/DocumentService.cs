using HIVE.Server.Data;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly DataContext _context;

        public DocumentService(DataContext context)
        {
            _context = context;
        }
        public async Task ArchiveDocument(int id)
        {
            var result = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.IsArchived == false);
            if (result is not null)
            {
                result.IsArchived = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UnArchiveDocument(int id)
        {
            var result = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.IsArchived == true);
            if (result is not null)
            {
                result.IsArchived = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateDocumentStatusAsync(int id)
        {
            try
            {
                var result = await _context.Documents.FindAsync(id);
                if (result != null)
                {
                    if (result.ToReview && result.UnApproved == false)
                    {
                        result.ToReview = false;
                    }
                    else if (result.ToReview == false && result.UnApproved == false)
                    {
                        result.UnApproved = true;
                    }
                    else
                    {
                        result.ToReview = true;
                        result.UnApproved = false;
                    }
                }
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task MoveToTrash(int id)
        {
            var response = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.IsDeleted == false);
            if (response is not null)
            {
                response.IsDeleted = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var response = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.IsDeleted == true);
            if (response is not null)
            {
                response.IsDeleted = false;
            }
            await _context.SaveChangesAsync();
        }
    }
}
