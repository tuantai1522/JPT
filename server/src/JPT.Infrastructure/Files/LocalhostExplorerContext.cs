using JPT.Core.Features.Files;
using JPT.UseCases.Abstractions.Files;
using Microsoft.AspNetCore.Http;

namespace JPT.Infrastructure.Files
{
    public class LocalhostExplorerContext : IFileExplorerContext
    {
        public async Task<(string path, UploadFileType uploadFileType)> UploadFileAsync(Stream stream, string fileName)
        {
            var uploadsAbs = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsAbs);
            
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            
            var absPath = Path.Combine(uploadsAbs, uniqueFileName);
            
            await using var fs = new FileStream(absPath, FileMode.Create, FileAccess.Write, FileShare.None, 64 * 1024, useAsync: true);
            await stream.CopyToAsync(fs);
            
            var relativePath = Path.Combine("Uploads", uniqueFileName).Replace('\\', '/');
            
            return (relativePath, UploadFileType.LocalHost);
        }


        public Task<bool> DeleteFileAsync(string path)
        {
            return Task.FromResult(true);
        }
    }
}