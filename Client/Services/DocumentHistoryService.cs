using System.Net.Http.Json;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;

namespace HIVE.Client.Services
{
    public class DocumentHistoryService : IDocumentHistoryService
    {
        private readonly HttpClient _client;

        public DocumentHistoryService(HttpClient client)
        {
            _client = client;
        }
        public async Task<HttpResponseMessage>  InsertDocumentModified(DocumentHistory modified)
        {
            var response = await _client.PostAsJsonAsync("api/History/write/", modified);
            return response;
            
        }

        public async Task<HttpResponseMessage> GetDocumentHistories(int id)
        {
            var response = await _client.GetAsync($"api/History/{id}");
            return response;
        }
    }
}
