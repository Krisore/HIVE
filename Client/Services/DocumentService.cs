using System.Net.Http.Headers;
using System.Net.Http.Json;
using HIVE.Client.Authentication;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;

namespace HIVE.Client.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _client;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;

        public DocumentService(HttpClient client, CustomAuthenticationStateProvider customAuthenticationStateProvider)
        {
            _client = client;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
        }

        public List<Document>? Documents { get; set; } = new();
        public List<Document>? MyDocuments { get; set; } = new();

        public async Task<HttpResponseMessage> GetDocumentsAsync()
        {
            var response = await _client.GetAsync("api/Document");
            if (response.IsSuccessStatusCode)
            {
                await SetDocuments(response);
            }
            return response;
        }

        public async Task<List<Document>> GetMyDocumentsAsync(string owner)
        {
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetFromJsonAsync<List<Document>>($"api/Document/{owner}");
           return  response;
        }

        private async Task SetDocuments(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                Documents = await message.Content.ReadFromJsonAsync<List<Document>>();
            }
        }
        private async Task SetMyDocuments(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                Documents = await message.Content.ReadFromJsonAsync<List<Document>>();
            }
        }
    }
}
