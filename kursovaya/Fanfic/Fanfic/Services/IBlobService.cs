using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fanfic.Services
{
    public interface IBlobService
    {
        string ConnectionString { get; }
        BlobServiceClient blobServiceClient { get; }

        public Task<string> UploadToBlobContainerAsync(IFormFile file, string blobName, string containerName);
        public Task DeleteChapterImageFromBlobsIfExists(long chapterId);
    }
}
