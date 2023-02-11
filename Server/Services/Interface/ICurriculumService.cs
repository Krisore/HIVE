using HIVE.Shared.Model;

namespace HIVE.Server.Services.Interface
{
    public interface ICurriculumService
    {
        Task<IEnumerable<Curriculum>> GetCurriculumBySearch(string value);
        Task<Curriculum> GetCurriculumById(int value);
    }
}
