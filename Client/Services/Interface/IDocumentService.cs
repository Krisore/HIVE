using HIVE.Shared.Model;

namespace HIVE.Client.Services.Interface
{
    public interface IDocumentService
    {
        List<Document>? MyDocuments { get; set; }
        List<Document>? Documents { get; set; }
        Task<HttpResponseMessage> GetDocumentsAsync();
        Task<List<Document>> GetMyDocumentsAsync(string owner);
    }
}
