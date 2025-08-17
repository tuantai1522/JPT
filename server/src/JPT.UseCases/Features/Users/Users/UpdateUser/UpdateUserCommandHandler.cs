using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserProvider userProvider,
    IUserRepository userRepository): IRequestHandler<UpdateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Company);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }
        
        var result = user.UpdateUser(command.FirstName, command.MiddleName, command.LastName, command.AvatarId, command.Description, command.CompanyName, command.CompanyDescription, command.LogoId);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}
