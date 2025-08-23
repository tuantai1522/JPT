namespace JPT.UseCases.Features.Users.Users.GetCurrentUser;

public sealed record GetCurrentUserResponse(
    Guid Id,
    string Email,
    string Role,
    string Token
);
