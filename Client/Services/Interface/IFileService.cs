using HIVE.Shared.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace HIVE.Client.Services.Interface
{
    public interface IFileService
    {
        List<FileEntry> FileEntries { get; set; }
        FileEntry File { get; set; }
        Task<HttpResponseMessage> UploadFileAsync(IBrowserFile? fileEntries);
        Task SetFiles(HttpResponseMessage result);
        Task<List<FileEntry>> GetAllFiles();
        Task<HttpResponseMessage> GetStoredFileNameAsync(string storedFileName);
        Task<FileEntry?> GetFileById(int id);
        Task<HttpResponseMessage> DeleteFileAsync(int id);
    }
}
