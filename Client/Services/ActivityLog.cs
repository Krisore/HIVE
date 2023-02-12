using System.Net.Http.Json;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;

namespace HIVE.Client.Services
{
    public class ActivityLog : IActivityLog
    {
        private readonly HttpClient _client;

        public ActivityLog(HttpClient client)
        {
            _client = client;
        }
        public async Task<HttpResponseMessage> WriteLog(string actor, string detail, string type, string role)
        {
            var log = new Activity
            {
                Actor = actor,
                Detailed = detail,
                Type = type,
                Role = role
            };
            var response = await _client.PostAsJsonAsync($"api/Activity/write", log);
            return response;
        }

        public async Task<List<Activity>> GetLogs()
        {
            var response = await _client.GetFromJsonAsync<List<Activity>>("api/Activity/logs");
            return response;
        }

        public async Task<HttpResponseMessage> ClearLogs(int id)
        {
            var response = await _client.DeleteAsync($"api/Activity/logs/delete/{id}");
            return response;
        }
    }
}
