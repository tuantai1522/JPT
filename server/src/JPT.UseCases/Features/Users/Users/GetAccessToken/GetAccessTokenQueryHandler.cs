using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Abstractions.WebStorages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Users.Users.GetAccessToken;

internal sealed class GetAccessTokenQueryHandler(
    IApplicationDbContext dbContext,
    ICookieService cookieService,
    IUserRepository userRepository, 
    ITokenProvider tokenProvider): IRequestHandler<GetAccessTokenQuery, Result<GetAccessTokenResponse>>
{
    public async Task<Result<GetAccessTokenResponse>> Handle(GetAccessTokenQuery query, CancellationToken cancellationToken)
    {
        var refreshToken = cookieService.Get(Constant.RefreshTokenCookieName);

        var token = await GetRefreshTokenByToken(refreshToken, cancellationToken);

        if (token is null || token.ExpiredAt < DateTime.UtcNow)
        {
            return Result.Failure<GetAccessTokenResponse>(UserErrors.InvalidRefreshToken);
        }

        var user = await userRepository.GetUserByIdAsync(token.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<GetAccessTokenResponse>(UserErrors.InvalidRefreshToken);
        }
        
        string accessToken = tokenProvider.CreateAccessToken(user);
        
        return Result.Success(new GetAccessTokenResponse(accessToken));
    }

    private async Task<RefreshToken?> GetRefreshTokenByToken(string? token, CancellationToken cancellationToken)
    {
        var result = await dbContext.Set<RefreshToken>()
            .Where(c => c.Token == token)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

}
