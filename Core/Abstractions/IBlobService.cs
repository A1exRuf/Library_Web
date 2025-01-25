namespace Core.Abstractions;

public interface IBlobService
{
    public Task<string> UploadAsync(Stream stream, string fileName, string containerName);

    public Task DeleteAsync(string url);
}
