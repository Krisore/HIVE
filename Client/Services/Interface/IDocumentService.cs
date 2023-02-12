using HIVE.Shared.Model;
using HIVE.Shared.Request;

namespace HIVE.Client.Services.Interface
{
    public interface IDocumentService
    {
        Document Document { get; set; }
        List<Document>? MyDocuments { get; set; }
        int DocumentId { get; set; }
        List<Document>? Documents { get; set; }
        Task<HttpResponseMessage> GetDocumentsAsync();
        Task<List<Document>> GetDocumentsForArchivistAsync();
        Task<Document> UpdateDocumentAsync(int id, UploadDocumentRequest document);
        Task<UploadDocumentRequest> GetDocumentByDataTransfer(int id);
        Task<List<Document>?> GetAllDocuments();
        Task<Document> GetDocumentAsyncById(int id);
        Task<UploadDocumentRequest> GetDocumentRequestAsyncById(int id);
        Task<HttpResponseMessage> GetDeleteDocumentById(int id);
        Task<IEnumerable<Document>> GetMyDocumentsAsync(string owner);
        Task<HttpResponseMessage> RequestUploadDocumentAsync(UploadDocumentRequest request);
        Task<HttpResponseMessage> ArchiveDocumentAsync(int id);
        Task<HttpResponseMessage> UnArchiveDocumentAsync(int id);
        Task<HttpResponseMessage> RestoreDocument(int id);
        Task<HttpResponseMessage> MoveToTrash(int id);
        Task<HttpResponseMessage> UpdateStatusDocumentAsync(int id);
        Task<HttpResponseMessage> UpdateActiveStatusDocumentAsync();

    }
}
