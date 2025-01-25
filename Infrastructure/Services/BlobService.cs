using Azure.Storage.Blobs;
using Core.Abstractions;

namespace Infrastructure.Services;

public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
    async Task<string> IBlobService.UploadAsync(Stream stream, string fileName, string containerName)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();
        await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(stream, overwrite: true);

        return blobClient.Uri.ToString();
    }

    async Task IBlobService.DeleteAsync(string url)
    {
        Uri fileUri = new Uri(url);
        var parts = fileUri.ToString().Split('/');
        string fileName = parts[parts.Length - 1];
        string containerName = parts[parts.Length - 2];

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.DeleteIfExistsAsync();
    }
}
