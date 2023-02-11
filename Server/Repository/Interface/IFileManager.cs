using HIVE.Shared.Model;

namespace HIVE.Server.Repository.Interface
{
    public interface IFileManager
    {
        Task<List<FileEntry>> GetFiles();
        Task<FileEntry> GetFileId(int id);
        Task<FileEntry> UploadFileAsync(List<IFormFile> files);
        Task<FileEntry> DownloadFileAsync(string fileName);
        Task DeleteFileAsync(int id);
    }
}
