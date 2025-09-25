namespace Shared.Interfaces.Services;

public interface IStorageService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    Task<Stream?> GetAsync(string fileName);
    Task DeleteAsync(string fileName);
}