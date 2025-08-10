using JPT.Core.Features.Files;

namespace JPT.UseCases.Options;

public interface IFileOptions
{
    UploadFileType DefaultUploadFile { get;}
    string FirebaseUrl { get;}
    string FirebaseBucketName { get;}
    string LocalhostUrl { get;}
}