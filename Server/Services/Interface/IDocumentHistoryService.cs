using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface IDocumentHistoryService
    {
        Task InsertEditLog(DocumentHistory modified);
    }
}
