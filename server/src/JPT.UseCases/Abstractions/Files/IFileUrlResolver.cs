using JPT.Core.Features.Files;

namespace JPT.UseCases.Abstractions.Files;

public interface IFileUrlResolver
{
    /// <summary>
    /// To build linkUrl return for user
    /// </summary>
    public string Build(UploadFileType type, string storagePath);
}