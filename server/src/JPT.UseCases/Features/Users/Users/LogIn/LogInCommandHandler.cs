using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.WebStorages;
using JPT.UseCases.Options;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.LogIn;

internal sealed class LogInCommandHandler(
    ITokenProvider tokenProvider,
    ICookieService cookieService,
    IJwtOptions jwtOptions,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher): IRequestHandler<LogInCommand, Result<LogInResponse>>
{
    public async Task<Result<LogInResponse>> Handle(LogInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindUserByEmailAsync(command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LogInResponse>(UserErrors.NotFoundByEmail);
        }
        
        bool verified = passwordHasher.Verify(command.Password, user.HashPassword);

        if (!verified)
        {
            return Result.Failure<LogInResponse>(UserErrors.InvalidPassword);
        }

        string accessToken = tokenProvider.CreateAccessToken(user);
        string refreshToken = tokenProvider.CreateRefreshToken(user);
        
        // Set cookie of refreshToken
        cookieService.Set(Constant.RefreshTokenCookieName, refreshToken, DateTimeOffset.UtcNow.AddMinutes(jwtOptions.ExpiredRefreshToken));

        return Result.Success(new LogInResponse(accessToken, user.Id, user.Email, user.Role.ToString()));
    }
}
