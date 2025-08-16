using JPT.Core.Common;
using JPT.Core.Features.Files;
using MediatR;

namespace JPT.UseCases.Features.Files.DeleteFile;

internal sealed class FileDeletedDomainEventHandler(
    IFileRepository fileRepository, 
    IUnitOfWork unitOfWork) : INotificationHandler<FileDeletedDomainEvent>
{
    public async Task Handle(FileDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var file = await fileRepository.GetFileByIdAsync(notification.FileId, cancellationToken);

        // Can't find file -> return
        if (file is null)
        {
            return;
        }
        
        // Call method Delete
        file.Delete();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
