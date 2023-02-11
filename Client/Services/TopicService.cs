using System.Net.Http.Json;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;

namespace HIVE.Client.Services
{
    public class TopicService : ITopicService
    {
        private readonly HttpClient _client;

        public TopicService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Topic>> GetTopics()
        {
            var result = await _client.GetFromJsonAsync<List<Topic>>($"api/Topic/");
            return result;
        }
    }
}
