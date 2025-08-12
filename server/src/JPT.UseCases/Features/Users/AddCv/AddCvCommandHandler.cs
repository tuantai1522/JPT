using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.AddCv;

internal sealed class AddCvCommandHandler(
    IUserProvider userProvider,
    IUserRepository userRepository) : IRequestHandler<AddCvCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddCvCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Cvs);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }

        var cv = Cv.CreateCv(userId, command.FileId);
        var result = user.AddCv(cv);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return userId;
    }
}
