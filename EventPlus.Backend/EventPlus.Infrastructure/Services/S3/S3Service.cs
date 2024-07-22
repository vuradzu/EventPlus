using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using EventPlus.Application.Options;
using EventPlus.Application.Services.S3;
using EventPlus.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;

namespace EventPlus.Infrastructure.Services.S3;

[Service]
public class S3Service : IS3Service
{
    private readonly AmazonS3Client _s3Client;
    private readonly S3Options _options;

    public S3Service(IOptions<S3Options> optionsAccessor)
    {
        _options = optionsAccessor.Value;

        var s3Config = new AmazonS3Config { ServiceURL = GetUploadFileUrl() };

        _s3Client = new AmazonS3Client(_options.AccessKey, _options.SecretKey, s3Config);
    }

    public async Task<string> UploadFile(BucketTypes type, string path, IFormFile file, string fileName)
    {
        var fileNameWithExtensionName = fileName + '.' + file.FileName.Split('.').Last();
        var fullPath = Path.Join(path, fileNameWithExtensionName);

        using var newMemoryStream = new MemoryStream();
        await file.CopyToAsync(newMemoryStream);

        var putObjectRequest = new PutObjectRequest
        {
            Key = fullPath,
            BucketName = GetBucketName(type),
            InputStream = newMemoryStream
        };

        var result = await _s3Client.PutObjectAsync(putObjectRequest);

        if (result.HttpStatusCode >= HttpStatusCode.MultipleChoices)
            throw new CustomHttpException("S3 upload error", result.HttpStatusCode, "S3Error");

        return GetPublicFileUrl(type, fullPath);
    }

    public async Task<string> UploadFileFromUrl(BucketTypes type, string path, string fileUrl, string fileName)
    {
        using HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(fileUrl);

        if (!response.IsSuccessStatusCode) throw new ValidationFailedException("Cannot download file");

        var fileContentType = response.Content.Headers.ContentType!.ToString();
        var fileExtension = fileContentType.Split("/").Last();
        
        var fileNameWithExtensionName = fileName + '.' + fileExtension;
        var fullPath = Path.Join(path, fileNameWithExtensionName);

        using var newMemoryStream = new MemoryStream();
        await (await response.Content.ReadAsStreamAsync()).CopyToAsync(newMemoryStream);

        var putObjectRequest = new PutObjectRequest
        {
            Key = fullPath,
            BucketName = GetBucketName(type),
            InputStream = newMemoryStream
        };

        var result = await _s3Client.PutObjectAsync(putObjectRequest);

        if (result.HttpStatusCode >= HttpStatusCode.MultipleChoices)
            throw new CustomHttpException("S3 upload error", result.HttpStatusCode, "S3Error");

        return GetPublicFileUrl(type, fullPath);
        
        
    }

    public async Task DeleteFile(BucketTypes type, string fileUrl)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            Key = GetFileKeyFromUrl(fileUrl),
            BucketName = GetBucketName(type)
        };

        var result = await _s3Client.DeleteObjectAsync(deleteObjectRequest);
        
        if (result.HttpStatusCode >= HttpStatusCode.MultipleChoices)
            throw new CustomHttpException("S3 delete error", result.HttpStatusCode, "S3Error");
    }


    private string GetBucketName(BucketTypes type) => type switch
    {
        BucketTypes.Users => _options.Buckets.UsersBucketName,
        BucketTypes.Commands => _options.Buckets.CommandsBucketName,
        _ => throw new ArgumentException("Wrong bucket type")
    };

    private string GetUploadFileUrl() => $"https://s3.{_options.Region}.{_options.Service}.com";
    
    private string GetPublicFileUrl(BucketTypes type, string fileName)
        => $"https://{GetBucketName(type)}.s3.{_options.Region}.{_options.Service}.com/{fileName}";

    private string GetFileKeyFromUrl(string fileUrl) => fileUrl.Split(".com/").Last();
}