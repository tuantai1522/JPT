using JPT.Core.Features.Files;
using JPT.UseCases.Abstractions.Files;
using JPT.UseCases.Options;
using Microsoft.AspNetCore.Http;

namespace JPT.Infrastructure.Files
{
    public class FileUrlResolver(IFileOptions fileOptions, IHttpContextAccessor httpContextAccessor) : IFileUrlResolver
    {
        public string Build(UploadFileType type, string storagePath)
        {
            return type switch
            {
                UploadFileType.Firebase  => BuildFirebaseUrl(storagePath),
                UploadFileType.LocalHost  => BuildLocalhostUrl(storagePath),
                _ => throw new NotSupportedException($"Unsupported type: {type}")
            };
        }
        
        private string BuildFirebaseUrl(string path)
        {
            var encoded = Uri.EscapeDataString(path); 
            
            return fileOptions.FirebaseStorageUrl
                .Replace("{FirebaseBucketName}", fileOptions.FirebaseBucketName)
                .Replace("{PathEncoded}", encoded);
        }
        
        private string BuildLocalhostUrl(string relativePath)
        {
            var request = httpContextAccessor.HttpContext?.Request;
            return request != null ? $"{request.Scheme}://{request.Host}/{relativePath}" : "";
        }
    }
}