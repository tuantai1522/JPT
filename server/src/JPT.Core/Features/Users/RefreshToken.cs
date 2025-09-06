using JPT.Core.Common;

namespace JPT.Core.Features.Users;

public sealed class RefreshToken : IBaseEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string Token { get; private set; } = null!;
    
    public Guid UserId { get; private set; }
    
    public DateTime ExpiredAt { get; private set; }

    private RefreshToken()
    {
        
    }

    internal static RefreshToken Create(string token, Guid userId, DateTime expiredAt)
    {
        return new RefreshToken
        {
            Token = token,
            UserId = userId,
            ExpiredAt = expiredAt,
        };
    }

    internal void UpdateExpiredAt(DateTime expiredAt) => ExpiredAt = expiredAt;
    internal void UpdateToken(string token) => Token = token;
}