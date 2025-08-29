using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.WebStorages;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.RefreshToken;

internal sealed class RefreshTokenQueryHandler(
    ICookieService cookieService,
    IUserRepository userRepository, 
    ITokenProvider tokenProvider): IRequestHandler<RefreshTokenQuery, Result<RefreshTokenResponse>>
{
    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenQuery query, CancellationToken cancellationToken)
    {
        var refreshToken = cookieService.Get(Constant.RefreshTokenCookieName);

        if (refreshToken is null)
        {
            return Result.Failure<RefreshTokenResponse>(UserErrors.NotFoundRefreshToken);
        }

        var principal = tokenProvider.VerifyRefreshToken(refreshToken);

        if (principal is null)
        {
            return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);
        }
        var userId = tokenProvider.GetUserIdFromClaimsPrincipal(principal);

        var user = await userRepository.GetUserByIdAsync(Guid.Parse(userId!), cancellationToken);

        string accessToken = tokenProvider.CreateAccessToken(user!);
        
        var response = new RefreshTokenResponse(accessToken);
        
        return Result.Success(response);
    }
}
