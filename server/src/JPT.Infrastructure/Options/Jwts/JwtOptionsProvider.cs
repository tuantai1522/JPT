using JPT.UseCases.Options;
using Microsoft.Extensions.Options;

namespace JPT.Infrastructure.Options.Jwts
{
    public sealed class JwtOptionsProvider(IOptionsMonitor<JwtOptions> jwtOptions) : IJwtOptions
    {
        public string AccessTokenKey => jwtOptions.CurrentValue.AccessTokenKey;
        public string RefreshTokenKey => jwtOptions.CurrentValue.RefreshTokenKey;
        public string Issuer => jwtOptions.CurrentValue.Issuer;
        public string Audience => jwtOptions.CurrentValue.Audience;
        public int ExpiredAccessToken => jwtOptions.CurrentValue.ExpiredAccessToken;
        public int ExpiredRefreshToken => jwtOptions.CurrentValue.ExpiredRefreshToken;
    }
}

