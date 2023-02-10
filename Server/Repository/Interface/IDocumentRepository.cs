using HIVE.Shared.Model;

namespace HIVE.Server.Repository.Interface
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetDocumentsAsync();
        Task<List<Document>> GetMyDocumentsAsync(string owner);
    
    }
}
