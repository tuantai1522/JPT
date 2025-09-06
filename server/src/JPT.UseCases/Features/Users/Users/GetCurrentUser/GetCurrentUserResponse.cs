namespace JPT.UseCases.Features.Users.Users.GetCurrentUser;

public sealed record GetCurrentUserResponse(
    Guid Id,
    string FirstName,
    string Role,
    string? AvatarUrl
);
