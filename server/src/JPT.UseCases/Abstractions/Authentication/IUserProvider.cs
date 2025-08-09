namespace JPT.UseCases.Abstractions.Authentication;

public interface IUserProvider
{
    /// <summary>
    /// Get UserId from Jwt token.
    /// </summary>
    Guid UserId { get; }
}