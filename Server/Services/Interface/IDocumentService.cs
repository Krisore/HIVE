using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface IDocumentService
    {
        Task ArchiveDocument(int id);
        Task UnArchiveDocument(int id);
        Task UpdateDocumentStatusAsync(int id);
        Task MoveToTrash(int id);
        Task Restore(int id);
    }
}
