namespace JPT.UseCases.Options;

public interface IJwtOptions
{
    string AccessTokenKey { get; }
    string RefreshTokenKey { get; }
    string Issuer { get; }
    string Audience { get; }
    int ExpiredAccessToken { get; }
    int ExpiredRefreshToken { get; }
}