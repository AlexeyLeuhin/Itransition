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

        public Task<string> UploadAvatar(IFormFile file, string blobName);
    }
}
