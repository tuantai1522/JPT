using System.Security.Claims;
using JPT.Core.Features.Users;

namespace JPT.UseCases.Abstractions.Authentication;

/// <summary>
/// To create Jwt token for user.
/// </summary>
public interface ITokenProvider
{
    string CreateAccessToken(User user);
    string CreateRefreshToken(User user);

    ClaimsPrincipal? VerifyRefreshToken(string token);

    string? GetUserIdFromClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
}