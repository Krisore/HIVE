using System.Collections;
using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface IReferenceService
    {
        Task<IEnumerable> GetReferenceBySearch(string search);
        Task<Reference> GetReference(int value);
        Task<List<Reference>> GetReferences();
        Task AddReferenceAsync(Reference reference);
        Task EditReferencesAsync(int id, Reference reference);
        Task DeleteReferenceAsync(int id);
    }
}
