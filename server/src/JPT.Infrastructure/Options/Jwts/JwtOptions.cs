using JPT.UseCases.Options;

namespace JPT.Infrastructure.Options.Jwts
{
    public sealed class JwtOptions : IJwtOptions
    {
        public string AccessTokenKey { get; init; } = null!;
        public string RefreshTokenKey { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public int ExpiredAccessToken { get; init; }
        public int ExpiredRefreshToken { get; init; }
    }
}

