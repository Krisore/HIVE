using HIVE.Shared.Model;
using HIVE.Shared.Request;

namespace HIVE.Server.Repository.Interface
{
    public interface IDocumentRepository
    {
        UploadDocumentRequest DataTransferObjectDocument { get; set; }
        Task<List<Document>> GetDocumentsAsync();
        Task<List<Document>> GetArchivedDocumentsAsync();
        Task<List<Document>> GetTrashedDocumentsAsync();
        Task<List<string>> DocumentsTitle();
        Task<IEnumerable<Document>> GetMyDocumentsAsync(string owner);
        Task DeleteAsync(int id);
        Task<Document> GetDocumentAsyncById(int id);
        Task DeleteDocumentAsync(int id);
        Task<Document> AddDocumentAsync(UploadDocumentRequest request);
        Task<Document> EditDocumentAsync(int id, UploadDocumentRequest document);
        Task<UploadDocumentRequest> GetDocumentDataTransferAsync(int id);
        Task<List<Document>> GetDocumentsForArchivist();
        Task ArchiveDocument(int id);
        Task MoveToTrashAsync(int id);
        Task UnArchiveDocument(int id);
        Task RestoreDocument(int id);
        Task UpdateDocumentStatus(int id);





    }
}
