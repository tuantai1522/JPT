using JPT.Core.Features.Users;

namespace JPT.UseCases.Abstractions.Authentication;

/// <summary>
/// To create Jwt token for user.
/// </summary>
public interface ITokenProvider
{
    string CreateAccessToken(User user);
    string CreateRefreshToken();
}