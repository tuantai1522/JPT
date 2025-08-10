using JPT.Core.Features.Files;
using JPT.UseCases.Options;

namespace JPT.Infrastructure.Options.Files
{
    public sealed class FileOptions : IFileOptions
    {
        public UploadFileType DefaultUploadFile { get; init; }
        public string FirebaseUrl { get; init; } = null!;
        public string FirebaseBucketName { get; init; } = null!;
        public string LocalhostUrl { get; init; } = null!;
    }
}

