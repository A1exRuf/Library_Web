namespace Core.Abstractions;

public interface IBlobService
{
    public Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);

    public Task<FileResponse> DownloadAsync(Guid imageId, CancellationToken cancellationToken = default);

    public Task DeleteAsync(Guid imageId, CancellationToken cancellationToken = default);
}
