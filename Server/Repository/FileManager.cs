using Azure.Storage.Blobs;
using HIVE.Server.Data;
using HIVE.Server.Repository.Interface;
using HIVE.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Repository
{
    public class FileManager : IFileManager
    {
        private readonly DataContext _context;
        private readonly string? _connectionString;
        
        public FileManager(DataContext context, BlobServiceClient blobService, IConfiguration builder)
        {
            _context = context;
            _connectionString = builder.GetConnectionString("ConnectionStrings:DefaultConnections:blob");
        }
        public async Task<List<FileEntry>> GetFiles()
        {
            var response = await _context.FileEntries.ToListAsync();
            return response;
        }

        public async Task<FileEntry> GetFileId(int id)
        {
            var response = await _context.FileEntries.SingleOrDefaultAsync(f => f.Id == id);
            return response;
        }

        public async Task<FileEntry> UploadFileAsync(List<IFormFile> files)
        {
            var uploadResults = new List<FileEntry>();
            var uploadResult = new FileEntry();
            var container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=puparch;AccountKey=ggiTXy86V3PzvZoDLvjSM9EiKIViz0WG1tPWxh16YTSg4NP2TqQBqMF+2/LUSKw/wnuW53rgsqEU+ASt5LmhUQ==;BlobEndpoint=https://puparch.blob.core.windows.net/;TableEndpoint=https://puparch.table.core.windows.net/;QueueEndpoint=https://puparch.queue.core.windows.net/;FileEndpoint=https://puparch.file.core.windows.net/", "pupstored");
            foreach (var file in files)
            {
                var untrustedFileName = file.FileName;
                var size = file.Length;

                var trustedFileNameForStorage = $"PUPBC_{Path.GetRandomFileName()}_{DateTime.Now.Year}";
                var blob = container.GetBlobClient(trustedFileNameForStorage);

                await using Stream stream = file.OpenReadStream();
                await blob.UploadAsync(stream);

                uploadResult.FileName = untrustedFileName;
                uploadResult.StoreFileName = trustedFileNameForStorage;
                uploadResult.ContentType = file.ContentType;
                uploadResult.Size = size;
                uploadResult.Status = true;
                uploadResults.Add(uploadResult);
            }
            _context.FileEntries.Add(uploadResult);
            await _context.SaveChangesAsync();
            return uploadResult;
        }


        public async Task<FileEntry> DownloadFileAsync(string fileName)
        {
            var uploadResult = await _context.FileEntries.FirstOrDefaultAsync(u => u.StoreFileName != null && u.StoreFileName.Equals(fileName));
            if (uploadResult == null)
            {
                throw new Exception(message: "uploadResult is null");
            }
            return uploadResult;
        }

        public async Task DeleteFileAsync(int id)
        {
            var container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=puparch;AccountKey=ggiTXy86V3PzvZoDLvjSM9EiKIViz0WG1tPWxh16YTSg4NP2TqQBqMF+2/LUSKw/wnuW53rgsqEU+ASt5LmhUQ==;BlobEndpoint=https://puparch.blob.core.windows.net/;TableEndpoint=https://puparch.table.core.windows.net/;QueueEndpoint=https://puparch.queue.core.windows.net/;FileEndpoint=https://puparch.file.core.windows.net/", "pupstored");
            var response = await _context.FileEntries.FirstOrDefaultAsync(f => f.Id == id);
            _context.FileEntries.Remove(response);
            var blob = container.GetBlobClient(response.StoreFileName);
            await blob.DeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
