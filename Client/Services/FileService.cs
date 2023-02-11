using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HIVE.Client.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _client;
        private readonly long _maxFileSize = long.MaxValue;
        public FileService(HttpClient client)
        {
            _client = client;
        }
        public List<FileEntry>? FileEntries { get; set; } = new();
        public FileEntry File { get; set; }
        public async Task<HttpResponseMessage> UploadFileAsync(IBrowserFile? file)
        {
            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream(_maxFileSize));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(
                content: fileContent,
                name: "\"files\"",
                fileName: file.Name);
           var response =   await _client.PostAsync($"api/File/", content);
           var result = await response.Content.ReadFromJsonAsync<FileEntry>();
           if (result is not null)
           {
               File = result;
           }
            return response;
        }
        public async Task<List<FileEntry>> GetAllFiles()
        {
            var result = await _client.GetFromJsonAsync<List<FileEntry>>("api/file");
            if (result is not null)
            {
                FileEntries = result;
            }

            Debug.Assert(FileEntries != null, nameof(FileEntries) + " != null");
            return FileEntries;
        }
        public async Task SetFiles(HttpResponseMessage result)
        {
            await result.Content.ReadFromJsonAsync<List<FileEntry>>();
            //_navigation.NavigateTo("files");
        }
        public async Task<HttpResponseMessage> GetStoredFileNameAsync(string storedFileName)
        {
            var response = await _client.GetAsync($"/api/File/{storedFileName}");
            return response;
        }
        public async Task<FileEntry?> GetFileById(int id)
        {
            var result = await _client.GetFromJsonAsync<FileEntry>($"api/File/{id}");
            return result;

        }
        public async Task<HttpResponseMessage> DeleteFileAsync(int id)
        {
            var result = await _client.DeleteAsync($"api/File/{id}");
            return result;
        }
    }
}
