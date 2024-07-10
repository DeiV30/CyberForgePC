using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace  cyberforgepc.Helpers.Storage
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("AzureBlobStorage:ConnectionString").Value;
            _containerName = configuration.GetSection("AzureBlobStorage:ContainerName").Value;
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = "image/jpeg" });

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteImageAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadImageAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var downloadInfo = await blobClient.DownloadAsync();
            return downloadInfo.Value.Content;
        }
    }
}
