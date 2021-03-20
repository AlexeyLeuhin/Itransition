using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Services
{
    public class BlobService : IBlobService
    {
        public BlobService(IConfiguration configuration)
        {    
            ConnectionString = configuration["Blob-service-connection-string"];
            blobServiceClient = new BlobServiceClient(ConnectionString);
        }

        public string ConnectionString { get; }
        public BlobServiceClient blobServiceClient { get; }

        public async Task<string> UploadToBlobContainerAsync(IFormFile file, string blobName, string containerName)
        {
            BlobClient blobClient = blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);
            await blobClient.UploadAsync(file.OpenReadStream(), true);
            return blobClient.Uri.AbsoluteUri;
        }

        public async Task DeleteChapterImageFromBlobsIfExists(long chapterId)
        {
            await DeleteBlobIfExists("chapter-" + chapterId, "chapterimagecontainer");
        }

        private async Task DeleteBlobIfExists(string blobName, string containerName)
        {
            BlobClient blobClient = blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
