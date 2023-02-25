using HIVE.Shared.Model;

namespace HIVE.Client.Services.Interface
{
    public interface IDocumentHistoryService
    {
        Task<HttpResponseMessage> InsertDocumentModified(DocumentHistory modified);
    }
}
