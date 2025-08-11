using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IUserProvider userProvider,
    IUserRepository userRepository): IRequestHandler<UpdateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(
            userId,
            cancellationToken,
            u => u.Avatar,
            u => u.Company,
            u => u.Company!.Logo
        );

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }
        
        user.UpdateUser(command.FirstName, command.MiddleName, command.LastName, command.AvatarId, command.CompanyName, command.CompanyDescription, command.LogoId);
        
        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(user.Id);
    }
}
