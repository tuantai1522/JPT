using JPT.Core.Features.Files;
using JPT.UseCases.Options;
using Microsoft.Extensions.Options;

namespace JPT.Infrastructure.Options.Files
{
    public sealed class FileOptionsProvider(IOptionsMonitor<FileOptions> fileOptions) : IFileOptions
    {
        public UploadFileType DefaultUploadFile => fileOptions.CurrentValue.DefaultUploadFile;
        public string FirebaseUrl => fileOptions.CurrentValue.FirebaseUrl;
        public string FirebaseBucketName => fileOptions.CurrentValue.FirebaseBucketName;
        public string LocalhostUrl => fileOptions.CurrentValue.LocalhostUrl;
    }
}

