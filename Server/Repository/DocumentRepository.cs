using HIVE.Server.Data;
using HIVE.Server.Repository.Interface;
using HIVE.Shared.Model;
using HIVE.Shared.Request;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Transactions;
using HIVE.Server.Services.Interface;
using Document = HIVE.Shared.Model.Document;
using System.Xml.Linq;

namespace HIVE.Server.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;
        private readonly IFileManager _fileManager;
        private readonly IDocumentService _documentService;

        public DocumentRepository(DataContext context, IFileManager fileManager, IDocumentService documentService)
        {
            _context = context;
            _fileManager = fileManager;
            _documentService = documentService;
        }
        public UploadDocumentRequest DataTransferObjectDocument { get; set; } = new UploadDocumentRequest();
        public async Task<List<Document>> GetDocumentsForArchivist()
        {
            try
            {
                var response = await _context.Documents.Where(d => d.IsArchived == false && d.IsDeleted == false)
                    .Include(a => a.Authors)
                    .Include(a => a.Adviser)
                    .Include(t => t.Topics)
                    .Include(a => a.Reference)
                    .Include(d => d.Curriculum)
                    .Include(f => f.File)
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mabe the documents is null {ex.HelpLink}");
                ex.HelpLink = "Please contact: kristiandizon.enore@gmail.com";
                throw;
            }
        }
        public async Task ArchiveDocument(int id)
        {
            await _documentService.ArchiveDocument(id);
        }

        public async Task UnArchiveDocument(int id)
        {
            await _documentService.UnArchiveDocument(id);
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _context.Documents.FirstOrDefaultAsync(D => D.Id == id);
            if (response is not null)
            {
                _context.Documents.Remove(response);
                await _fileManager.DeleteFileAsync(response.Id);
            }
            await  _context.SaveChangesAsync();
        }
        public async Task MoveToTrashAsync(int id)
        {
            await _documentService.MoveToTrash(id);
        }

        public async Task RestoreDocument(int id)
        {
            await _documentService.Restore(id);
        }
        public async Task<List<Document>> GetArchivedDocumentsAsync()
        {
            var result = await _context.Documents.Where(d => d.IsArchived == true && d.IsDeleted == false)
                .Include(d => d.Adviser)
                .Include(d => d.Reference)
                .Include(t => t.Topics)
                .Include(a => a.Curriculum)
                .Include(a => a.Authors)
                .ToListAsync();
            return result;
        }

        public async Task<List<Document>> GetTrashedDocumentsAsync()
        {
            var result = await _context.Documents.Where(d => d.IsDeleted == true)
                .Include(d => d.Adviser)
                .Include(t => t.Topics)
                .Include(a => a.Curriculum)
                .Include(d => d.Reference)
                .Include(a => a.Authors)
                .ToListAsync();
            return result;
        }
        public async Task UpdateDocumentStatus(int id)
        {
            await _documentService.UpdateDocumentStatusAsync(id);
        }
        public async Task<Document> GetDocumentAsyncById(int id)
        {
            try
            {
                var result = await _context.Documents.Where(d => d.Id == id)
                    .Include(d => d.Adviser)
                    .Include(f => f.File)
                    .Include(d => d.Reference)
                    .Include(a => a.Authors)
                    .Include(t => t.Topics)
                    .FirstOrDefaultAsync();
                return result ?? throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }

        }

        public async Task DeleteDocumentAsync(int id)
        {
            try
            {
                var document = await _context.Documents.FindAsync(id);
                if (document == null)
                {
                    throw new NullReferenceException(message: "Null occured");
                }

                document.IsDeleted = true;
                _context.Documents.Remove(document);
                await _fileManager.DeleteFileAsync(document.FileId);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<List<string>> DocumentsTitle()
        {
            var response = await _context.Documents.Select(d => d.Title).ToListAsync();
            return response;
        }
        public async Task<List<Document>> GetDocumentsAsync()
        {
            try
            {
                var response = await _context.Documents
                    .Where(d => d.ToReview == false
                                && d.UnApproved == false
                                && d.IsActive == true
                                && d.IsArchived == false
                                && d.IsDeleted == false)
                    .Include(t => t.Topics)
                    .Include(a => a.Authors)
                    .Include(f => f.File)
                    .Include(a => a.Curriculum)
                    .Include(d => d.Reference)
                    .Include(a => a.Authors)
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mabe the documents is null {ex.HelpLink}");
                ex.HelpLink = "Please contact: kristiandizon.enore@gmail.com";
                throw;
            }
            
        }

       

        public async Task<IEnumerable<Document>> GetMyDocumentsAsync(string owner)
        {
            var result = await _context.Documents
                .Where(d => d.UploaderEmail == owner && d.IsDeleted == false && d.IsArchived == false)
                .Include(c => c.Curriculum)
                .Include(adviser => adviser.Adviser)
                .Include(t => t.Topics)
                .Include(r => r.Reference)
                .Include(a => a.Authors)
                .ToListAsync();
            return result;
        }

        public async Task<Document> AddDocumentAsync(UploadDocumentRequest request)
        {
            var adviser = await _context.Advisers.FindAsync(request.AdviserId);
            var curriculum = await _context.Curriculums.FindAsync(request.CurriculumId);
            var reference = await _context.References.FindAsync(request.ReferenceId);
            var file = await _context.FileEntries.FindAsync(request.FileId);
            if (adviser == null || curriculum == null || reference == null || file == null)
            {
                throw new Exception("Adviser, curriculum, reference, or file not found");
            }
            var document = new Document
            {
                Title = request.Title,
                Abstract = request.Abstract,
                DatePublished = request.DatePublished,
                AdviserId = request.AdviserId,
                CurriculumId = request.CurriculumId,
                ReferenceId = request.ReferenceId,
                UploaderEmail = request.UploaderEmail,
                Adviser = adviser,
                Curriculum = curriculum,
                Reference = reference,
                FileId = reference.Id,
                File = file,
            };
            var tenYearsAgo = DateTime.Now.AddYears(-10);
            if (document.DatePublished.HasValue && document.DatePublished < tenYearsAgo)
            {
                document.IsActive = false;
            }
            else
            {
                document.IsActive = true;
            }
            foreach (var topic in request.Topics)
            {
                if (topic.Name != null)
                {
                    var addTopic = await _context.Topics.FirstOrDefaultAsync(t => t.Name == topic.Name);
                    if (addTopic == null)
                    {
                        addTopic = new Topic { Name = topic.Name };
                        _context.Topics.Add(addTopic);
                    }

                    document.Topics.Add(addTopic);
                }
            }
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> EditDocumentAsync(int id, UploadDocumentRequest document)
        {

            var dbDocument = await _context.Documents.Include(t => t.Topics)
                .Include(d => d.Reference)
                .Include(a => a.Authors).FirstOrDefaultAsync(d => d.Id == id);

            dbDocument.Id = document.Id;
            dbDocument.Title = document.Title;
            dbDocument.Abstract = document.Abstract;
            dbDocument.DatePublished = document.DatePublished;
            dbDocument.CurriculumId = document.CurriculumId;
            dbDocument.AdviserId = document.AdviserId;
            dbDocument.ReferenceId = document.ReferenceId;
            dbDocument.UploaderEmail = document.UploaderEmail;
            var tenYearsAgo = DateTime.Now.AddYears(-10);
            if (dbDocument.DatePublished.HasValue && dbDocument.DatePublished < tenYearsAgo)
            {
                dbDocument.IsActive = false;
            }
            else
            {
                dbDocument.IsActive = true;
            }

            var file = await _context.FileEntries.FindAsync(document.FileId);
            if (file != null)
            {
                dbDocument.File = file;
            }

            foreach (var newTopic in document.Topics)
            {
                if (newTopic.Name != null)
                {
                    var topicTrans = new Topic()
                    {
                        Name = newTopic.Name
                    };
                    var topic = await _context.Topics
                        .Where(t => t.Name.Contains(topicTrans.Name, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefaultAsync();
                    if (topic != null)
                    {
                        dbDocument.Topics.Add(topicTrans);
                    }

                    topic.Documents.Add(dbDocument);
                }

            }
            await _context.SaveChangesAsync();
            return dbDocument;

        }
        public async Task<UploadDocumentRequest> GetDocumentDataTransferAsync(int id)
        {
            var document = await _context.Documents
                .Where(d => d.Id == id)
                .Include(d => d.Reference)
                .Include(a => a.Authors)
                .Include(t => t.Topics)
                .FirstOrDefaultAsync();
            if (document == null) return DataTransferObjectDocument;
            DataTransferObjectDocument = new UploadDocumentRequest
            {
                Title = document.Title,
                Abstract = document.Abstract,
                DatePublished = document.DatePublished,
                CurriculumId = document.CurriculumId,
                AdviserId = document.AdviserId,
                ReferenceId = document.ReferenceId,
                IsActive = document.IsActive,
                IsOpenAccess = document.IsOpenAccess,
            };
            var file = await _context.FileEntries.FindAsync(document.FileId);
            if (file != null)
            {
                DataTransferObjectDocument.FileId = file.Id;
            }
            foreach (var topicId in document.Topics)
            {
                var topic = await _context.Topics.FindAsync(topicId.Id);
                if (topic != null)
                {
                    var newTopic = new CreateTopic()
                    {
                        Id = topic.Id,
                        Name = topic.Name,
                    };
                    DataTransferObjectDocument.Topics.Add(newTopic);
                }
            }
            return DataTransferObjectDocument;
        }
    }
}
