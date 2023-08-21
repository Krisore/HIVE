using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface IDocumentHistoryService
    {
        Task<DocumentHistory> InsertEditLog(DocumentHistory modified);
        Task<List<DocumentHistory>> GetDocumentHistoriesById(int id);
    }
}
