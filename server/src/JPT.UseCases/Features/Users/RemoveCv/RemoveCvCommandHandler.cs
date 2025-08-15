using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.RemoveCv;

internal sealed class RemoveCvCommandHandler(
    IUnitOfWork unitOfWork,
    IUserProvider userProvider,
    IFileRepository fileRepository,
    IUserRepository userRepository) : IRequestHandler<RemoveCvCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RemoveCvCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Cvs);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }

        var file = await fileRepository.GetFileByIdAsync(command.FileId, cancellationToken);
        
        if (file is null || file.IsDeleted)
        {
            return Result.Failure<Guid>(FileErrors.NotFound(command.FileId));
        }
        
        var result = user.RemoveCv(file);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return userId;
    }
}
