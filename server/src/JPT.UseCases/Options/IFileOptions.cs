using JPT.Core.Features.Files;

namespace JPT.UseCases.Options;

public interface IFileOptions
{
    UploadFileType DefaultUploadFile { get;}
    string FirebaseStorageUrl { get;}
    string FirebaseBucketName { get;}
}