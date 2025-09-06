using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace JPT.Infrastructure.Authentication;

internal sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string CreateAccessToken(User user)
    {
        string accessTokenKey = configuration["JwtOptions:AccessTokenKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtOptions:ExpiredAccessToken")),
            SigningCredentials = credentials,
            Issuer = configuration["JwtOptions:Issuer"],
            Audience = configuration["JwtOptions:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string CreateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

}