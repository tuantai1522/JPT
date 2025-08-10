using Firebase.Storage;
using JPT.Core.Features.Files;
using JPT.UseCases.Abstractions.Files;
using Microsoft.Extensions.Options;
using FileOptions = JPT.Infrastructure.Options.Files.FileOptions;

namespace JPT.Infrastructure.Files
{
    public class FirebaseExplorerContext(IOptionsMonitor<FileOptions> fileOptions) : IFileExplorerContext
    {
        private readonly FirebaseStorage _storage = new(fileOptions.CurrentValue.FirebaseBucketName);

        public async Task<(string path, UploadFileType uploadFileType)> UploadFileAsync(Stream stream, string fileName)
        {
            var firebaseFilePath = $"{Guid.NewGuid()}";

            await _storage.Child(firebaseFilePath).PutAsync(stream);
            
            return (firebaseFilePath, UploadFileType.Firebase);
        }
    
        public async Task<bool> DeleteFileAsync(string path)
        {
            await _storage.Child(path).DeleteAsync();
            
            return true;
        }
    }
}