using Shared.Interfaces.Services;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Services;

public class MinioStorageService : IStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName = Environment.GetEnvironmentVariable("MINIO_BUCKET_NAME")!;

    public MinioStorageService()
    {
        var host = Environment.GetEnvironmentVariable("MINIO_HOST");
        var accessKey = Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY");
        var secretKey = Environment.GetEnvironmentVariable("MINIO_SECRET_KEY");

        _minioClient = new MinioClient()
            .WithEndpoint(host, 9000)
            .WithCredentials(accessKey, secretKey)
        .Build();

        _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
    }


    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var args = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(args);

        return $"{_bucketName}/{fileName}";
    }

    public async Task<Stream?> GetAsync(string fileName)
    {
        var ms = new MemoryStream();

        await _minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream => stream.CopyTo(ms)));

        ms.Position = 0;

        return ms;
    }

    public async Task DeleteAsync(string fileName)
        => await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName));
}