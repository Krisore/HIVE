using HIVE.Shared.Model;
using HIVE.Shared.Request;

namespace HIVE.Server.Repository.Interface
{
    public interface IDocumentRepository
    {
        UploadDocumentRequest DataTransferObjectDocument { get; set; }
        Task<List<Document>> GetDocumentsAsync();
        Task<IEnumerable<Document>> GetMyDocumentsAsync(string owner);
        Task<Document> GetDocumentAsyncById(int id);
        Task DeleteDocumentAsync(int id);
        Task<Document> AddDocumentAsync(UploadDocumentRequest request);
        Task<Document> EditDocumentAsync(int id, UploadDocumentRequest document);
        Task<UploadDocumentRequest> GetDocumentDataTransferAsync(int id);
        Task<List<Document>> GetDocumentsForArchivist();




    }
}
