using HIVE.Shared.Model;
using System;

namespace HIVE.Client.Services.Interface
{
    public interface ICurriculumService
    {
        List<Curriculum> Curriculums { get; set; }
        Task<IEnumerable<Curriculum>> GetCurriculumBySearch(string value);
        Task<Curriculum> GetCurriculumById(int id);
        Task<List<Curriculum>> GetProgramsAsync();

    }
}
