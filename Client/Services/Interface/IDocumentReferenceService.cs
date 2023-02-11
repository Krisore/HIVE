using HIVE.Shared.Model;

namespace HIVE.Client.Services.Interface
{
    public interface IDocumentReferenceService
    {
        Task<IEnumerable<Reference>> GetReferencesAsync(string value);
        Task<Reference> GetReferenceById(int id);
        Task<List<Reference>> GetReferences();


    }
}
