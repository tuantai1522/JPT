using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.Cv.RemoveCv;

internal sealed class RemoveCvCommandHandler(
    IUnitOfWork unitOfWork,
    IUserProvider userProvider,
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

        var result = user.RemoveCv(command.FileId);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return userId;
    }
}
