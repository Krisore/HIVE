using System.Net.Http.Json;
using HIVE.Client.Pages;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;
using System.Xml.Linq;

namespace HIVE.Client.Services
{
    public class DocumentReferenceService : IDocumentReferenceService
    {
        private readonly HttpClient _client;

        public DocumentReferenceService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<Reference>> GetReferencesAsync(string value)
        {
            var response = await _client.GetFromJsonAsync<List<Reference>>($"api/References/{value}");
            return response;
        }
        public async Task<Reference> GetReferenceById(int id)
        {
            var response = await _client.GetFromJsonAsync<Reference>($"api/References/{id}");
            return response;
        }
        public async Task<List<Reference>> GetReferences()
        {
            var response = await _client.GetFromJsonAsync<List<Reference>>("api/References");
            return response;
        }
    }
}
