using System.Net.Http.Headers;
using System.Net.Http.Json;
using HIVE.Client.Authentication;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;
using HIVE.Shared.Request;

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
        public int DocumentId { get; set; }
        public List<Document>? Documents { get; set; } = new();
        public Document Document { get; set; } = new();
        public List<Document>? MyDocuments { get; set; } = new();

        public async Task<HttpResponseMessage> GetDocumentsAsync()
        {
            var response = await _client.GetAsync("api/Document");
            return response;
        }

        public async Task<List<Document>> GetDocumentsForArchivistAsync()
        {
            try
            {
                var accessToken = await _customAuthenticationStateProvider.GetToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var result = await _client.GetFromJsonAsync<List<Document>>($"api/Document/archivist");
                if (result != null)
                {
                    Documents = result;
                }

                return Documents!;
                throw new Exception(message: "Documents are null!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task<Document> UpdateDocumentAsync(int id, UploadDocumentRequest document)
        {

            try
            {
                var result = await _client.PutAsJsonAsync($"api/Document/update/{document.Id}", document);
                if (result.IsSuccessStatusCode)
                {
                    Document = await result.Content.ReadFromJsonAsync<Document>() ?? throw new InvalidOperationException();
                }
                return Document;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task<UploadDocumentRequest> GetDocumentByDataTransfer(int id)
        {
            var response = await _client.GetFromJsonAsync<UploadDocumentRequest>($"api/Document/Get/{id}");
            return response;
        }

        public async Task<List<Document>?> GetAllDocuments()
        {
            var response = await _client.GetFromJsonAsync<List<Document>>("api/Document");
            if (response is not null)
            {
                Documents = response;
            }

            return Documents;
        }

        public async Task<Document> GetDocumentAsyncById(int id)
        {
            try
            {
                var response = await _client.GetFromJsonAsync<Document>($"api/Document/{id}");
                if (response != null)
                {
                    Document = response;
                }
                return Document;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task<UploadDocumentRequest> GetDocumentRequestAsyncById(int id)
        {
            var result = await _client.GetFromJsonAsync<UploadDocumentRequest>($"api/Document/Edit/{id}");
            return result;

        }

        public async Task<HttpResponseMessage> GetDeleteDocumentById(int id)
        {
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await _client.DeleteAsync($"api/Document/delete/{id}");
            return result;
        }

        public async Task<HttpResponseMessage> DeleteDocuments(int id)
        {
            var response = await _client.DeleteAsync($"api/Document/archivist/delete/{id}");
            return response;
        }


        public async Task<IEnumerable<Document>> GetMyDocumentsAsync(string owner)
        {
            var accessToken = await _customAuthenticationStateProvider.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetFromJsonAsync<List<Document>>($"api/Document/{owner}");
            return  response;
        }


        public async Task<HttpResponseMessage> RequestUploadDocumentAsync(UploadDocumentRequest request)
        { ;
            var response = await _client.PostAsJsonAsync("api/Document", request);
            var result = await response.Content.ReadFromJsonAsync<Document>();
            if (result != null)
            {
                Document = result;
            }
            return response;
        }
        public async Task<HttpResponseMessage> ArchiveDocumentAsync(int id)
        {
            var result = await _client.GetAsync($"api/Document/archivist/document/archived/{id}");
            return result;
        }

        public async Task<HttpResponseMessage> UnArchiveDocumentAsync(int id)
        {
            var response = await _client.GetAsync($"api/Document/archivist/document/unarchived/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> RestoreDocument(int id)
        {
            var response = await _client.GetAsync($"api/Document/archivist/document/restore/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> MoveToTrash(int id)
        {
            var response = await _client.GetAsync($"api/Document/archivist/document/trash/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> UpdateStatusDocumentAsync(int id)
        {
            var response = await _client.GetAsync($"api/Document/archivist/document/update/review/{id}");
            return response;
        }

        public Task<HttpResponseMessage> UpdateActiveStatusDocumentAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Document>> GetDocumentsInTrash()
        {
            var result = await _client.GetFromJsonAsync<List<Document>>($"api/Document/archivist/document/trash/");
            if (result == null) throw new ArgumentNullException(nameof(result));
            return result;
        }

        public async Task<List<Document>> GetArchivedDocuments()
        {
            var response = await _client.GetFromJsonAsync<List<Document>>($"api/Document/archivist/document/archived/");
            return response ?? throw new InvalidOperationException();
        }

    }
}
