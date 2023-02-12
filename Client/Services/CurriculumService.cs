using System;
using System.Net.Http.Json;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;

namespace HIVE.Client.Services
{
    public class CurriculumService : ICurriculumService
    {
        private readonly HttpClient _client;

        public CurriculumService(HttpClient client)
        {
            _client = client;
        }
        public List<Curriculum> Curriculums { get; set; } = new List<Curriculum>();
        public async Task<IEnumerable<Curriculum>> GetCurriculumBySearch(string value)
        {
            var response = await _client.GetFromJsonAsync<List<Curriculum>>($"api/Curriculums/{value}");
            return response;
        }


        public async Task<Curriculum> GetCurriculumById(int id)
        {
            var response = await _client.GetFromJsonAsync<Curriculum>($"api/Curriculums/{id}");
            return response;
        }
        public async Task<List<Curriculum>> GetProgramsAsync()
        {

            try
            {
                var response = await _client.GetFromJsonAsync<List<Curriculum>>("api/Curriculums");

                if (response != null)
                {
                    Curriculums = response;
                }

                return Curriculums;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public async Task<HttpResponseMessage> AddCurriculum(Curriculum? curriculum)
        {
            var result = await _client.PostAsJsonAsync("api/Curriculums", curriculum);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateAcademicProgram(Curriculum program)
        {
            var response = await _client.PutAsJsonAsync($"api/Curriculums/{program.Id}", program);
            return response;
        }
        public async Task<HttpResponseMessage> DeleteAcademicProgram(int id)
        {
            var result = await _client.DeleteAsync($"api/Curriculums/{id}");
            return result;
        }
    }
}
