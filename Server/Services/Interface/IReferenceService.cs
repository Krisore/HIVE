using System.Collections;
using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface IReferenceService
    {
        Task<IEnumerable> GetReferenceBySearch(string search);
        Task<Reference> GetReference(int value);
    }
}
