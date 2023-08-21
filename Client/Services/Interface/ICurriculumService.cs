using HIVE.Shared.Model;
using System;

namespace HIVE.Client.Services.Interface
{
    public interface ICurriculumService
    {
        List<Curriculum> Curriculums { get; set; }
        Task<IEnumerable<Curriculum>> GetCurriculumBySearch(string value);
        Task<HttpResponseMessage> AddCurriculum(Curriculum curriculum);
        Task<HttpResponseMessage> UpdateAcademicProgram(Curriculum program);
        Task<HttpResponseMessage> DeleteAcademicProgram(int id);

        Task<Curriculum> GetCurriculumById(int id);
        Task<List<Curriculum>> GetProgramsAsync();

    }
}
