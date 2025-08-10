using JPT.Core.Features.Files;

namespace JPT.UseCases.Abstractions.Files;

public interface IFileExplorerContext
{
    /// <summary>
    /// To return path after uploading successfully.
    /// </summary>
    Task<(string path, UploadFileType uploadFileType)> UploadFileAsync(Stream stream, string fileName);

    Task<bool> DeleteFileAsync(string path);
}