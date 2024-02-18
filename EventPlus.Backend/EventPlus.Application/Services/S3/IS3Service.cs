using Microsoft.AspNetCore.Http;

namespace EventPlus.Application.Services.S3;

public interface IS3Service
{
    Task<string> UploadFile(BucketTypes type, string path, IFormFile file, string fileName);
    Task DeleteFile(BucketTypes type, string fileUrl);
}