using Azure.Storage.Blobs;
using HIVE.Shared.Model;

namespace HIVE.Server.Repository.Interface
{
    public interface IAzureStorageHelper
    {
        string ContainerName { get; set; }

        BlobContainerClient OpenContainer(string containerName);
        Task<FileEntry> DownloadFileAsync(string fileName);
        Task<FileEntry> UploadFileAsync(List<IFormFile> files, string containerName, bool overwrite);
        Task<FileEntry> GetFileId(int id);
        Task<List<FileEntry>> GetFiles();
        Task DeleteFileAsync(int id, string containerName);
    }
}
