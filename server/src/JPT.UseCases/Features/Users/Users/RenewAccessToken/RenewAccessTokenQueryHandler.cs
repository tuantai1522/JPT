using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Abstractions.WebStorages;
using JPT.UseCases.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Users.Users.RenewAccessToken;

internal sealed class RenewAccessTokenQueryHandler(
    IJwtOptions jwtOptions,
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    ICookieService cookieService,
    IUserRepository userRepository, 
    ITokenProvider tokenProvider): IRequestHandler<RenewAccessTokenQuery, Result<RenewAccessTokenResponse>>
{
    public async Task<Result<RenewAccessTokenResponse>> Handle(RenewAccessTokenQuery query, CancellationToken cancellationToken)
    {
        var refreshToken = cookieService.Get(Constant.RefreshTokenCookieName);

        var token = await GetRefreshTokenByToken(refreshToken, cancellationToken);

        if (token is null || token.ExpiredAt < DateTime.UtcNow)
        {
            return Result.Failure<RenewAccessTokenResponse>(UserErrors.InvalidRefreshToken);
        }

        var user = await userRepository.GetUserByIdAsync(token.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<RenewAccessTokenResponse>(UserErrors.InvalidRefreshToken);
        }
        
        string accessToken = tokenProvider.CreateAccessToken(user);
        
        string newRefreshToken = tokenProvider.CreateRefreshToken();
        var expiredAt = DateTimeOffset.UtcNow.AddMinutes(jwtOptions.ExpiredRefreshToken);
        
        // Update new token and expiredAt
        user.UpdateRefreshToken(token.Id, newRefreshToken, expiredAt.UtcDateTime);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        // Set cookie of refreshToken in browser
        cookieService.Set(Constant.RefreshTokenCookieName, newRefreshToken, expiredAt);
        
        return Result.Success(new RenewAccessTokenResponse(accessToken));
    }

    private async Task<RefreshToken?> GetRefreshTokenByToken(string? token, CancellationToken cancellationToken)
    {
        var result = await dbContext.Set<RefreshToken>()
            .Where(c => c.Token == token)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

}
