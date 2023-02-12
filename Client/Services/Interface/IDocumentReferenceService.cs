using HIVE.Shared.Model;

namespace HIVE.Client.Services.Interface
{
    public interface IDocumentReferenceService
    {
        Task<IEnumerable<Reference>> GetReferencesAsync(string value);
        Task<Reference> GetReferenceById(int id);
        Task<List<Reference>> GetReferences();
        Task<HttpResponseMessage> AddReferenceAsync(Reference reference);
        Task<HttpResponseMessage> EditReferenceAsync(int id, Reference reference);
        Task<HttpResponseMessage> DeleteReferenceAsync(int id);

    }
}
