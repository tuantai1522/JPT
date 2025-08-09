using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher): IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var verifyEmail = await userRepository.VerifyExistedEmailAsync(command.Email, cancellationToken);

        if (verifyEmail)
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = User.CreateUser(
            command.FirstName,
            command.MiddleName, 
            command.LastName, 
            command.Email, 
            passwordHasher.Hash(command.Password),
            command.AvatarId,
            command.Role,
            command.CompanyName,
            command.LogoId);
        
        await userRepository.AddUserAsync(user, cancellationToken);

        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(user.Id);
    }
}
