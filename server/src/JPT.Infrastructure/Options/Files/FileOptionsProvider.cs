using JPT.Core.Features.Files;
using JPT.UseCases.Options;
using Microsoft.Extensions.Options;

namespace JPT.Infrastructure.Options.Files
{
    public sealed class FileOptionsProvider(IOptionsMonitor<FileOptions> fileOptions) : IFileOptions
    {
        public UploadFileType DefaultUploadFile => fileOptions.CurrentValue.DefaultUploadFile;
        public string FirebaseStorageUrl => fileOptions.CurrentValue.FirebaseStorageUrl;
        public string FirebaseBucketName => fileOptions.CurrentValue.FirebaseBucketName;
    }
}

