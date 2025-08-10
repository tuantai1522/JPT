using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.UseCases.Abstractions.Files;
using MediatR;
using File = JPT.Core.Features.Files.File;

namespace JPT.UseCases.Features.Files.UploadFile;

internal sealed class UploadFileCommandHandler(
    IFileRepository fileRepository, 
    IFileExplorerContext fileExplorerContext, 
    IFileUrlResolver fileUrlResolver) : IRequestHandler<UploadFileCommand, Result<UploadFileResponse>>
{
    public async Task<Result<UploadFileResponse>> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        var result = await fileExplorerContext.UploadFileAsync(command.Stream, command.FileName);

        var file = File.CreateFile(command.FileName, result.path, result.uploadFileType, command.MimeType);

        await fileRepository.AddFileAsync(file, cancellationToken);
        await fileRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        var path = fileUrlResolver.Build(result.uploadFileType, result.path);

        return new UploadFileResponse(file.Id, path);
    }
}
