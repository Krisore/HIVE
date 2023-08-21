using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HIVE.Server.Data;
using HIVE.Server.Repository.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace HIVE.Server.Repository
{
    public class AzureStorageHelper: IAzureStorageHelper
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private string _baseUrl;

        public AzureStorageHelper(IConfiguration configuration, DataContext context)
        {
            this._configuration = configuration;
            _context = context;
            _baseUrl = configuration["StorageBaseUrl"];
            ContainerName = "puphive";
        }
        public async Task<List<FileEntry>> GetFiles()
        {
            var response = await _context.FileEntries.ToListAsync();
            return response;
        }
        public async Task<FileEntry?> GetFileId(int id)
        {
            try
            {
                var response = await _context.FileEntries.FirstOrDefaultAsync(f => f.Id == id);
                if (response == null) return null;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public async Task<FileEntry> UploadFileAsync(List<IFormFile> files, string containerName, bool overwrite)
        {
            var uploadResults = new List<FileEntry>();
            var uploadResult = new FileEntry();
            var container = OpenContainer(containerName);
            try
            {
                foreach (var file in files)
                {
                    var untrustedFileName = file.FileName;
                    var size = file.Length;
                    var trustedFileNameForStorage = $"PUPBC_{Path.GetRandomFileName()}_{DateTime.Now.Year}";
                    var blob = container.GetBlobClient(trustedFileNameForStorage);
                    if (overwrite)
                    {
                        await blob.DeleteIfExistsAsync();
                    }
                    await using Stream stream = file.OpenReadStream();
                    await blob.UploadAsync(stream);
                    stream.Close();
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
        public async Task DeleteFileAsync(int id, string containerName)
        {
            var container = OpenContainer(containerName);
            var response = await _context.FileEntries.FirstOrDefaultAsync(f => f.Id == id);
            if (response != null)
            {
                _context.FileEntries.Remove(response);
                await _context.SaveChangesAsync();
                var blob = container.GetBlobClient(response.StoreFileName);
                await blob.DeleteAsync();
            }

        }

        public string ContainerName { get; set; }

        public BlobContainerClient OpenContainer(string containerName)
        {
            try
            {
                var setting = _configuration["StorageConnectionString"];
                //Create a blob service client object which will be used to create a container client
                BlobServiceClient blobServiceClient = new BlobServiceClient(setting);
                return blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                Console.WriteLine(msg);
                return null;
            }
        }
    }
}
