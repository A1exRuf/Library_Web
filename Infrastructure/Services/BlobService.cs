using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Abstractions;

namespace Infrastructure.Services;

public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
    private const string ContainerName = "images";

    async Task<Guid> IBlobService.UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        var imageId = Guid.NewGuid();
        BlobClient blobClient = containerClient.GetBlobClient(imageId.ToString());

        await blobClient.UploadAsync(
            stream, 
            new BlobHttpHeaders { ContentType = contentType }, 
            cancellationToken: cancellationToken);

        return imageId;
    }

    async Task<FileResponse> IBlobService.DownloadAsync(Guid imageId, CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(imageId.ToString());

        Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

        return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }

    async Task IBlobService.DeleteAsync(Guid imageId, CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(imageId.ToString());

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}
