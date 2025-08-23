namespace JPT.UseCases.Features.Users.Users.LogIn;

public sealed record LogInResponse(string Token, Guid Id, string Email, string UserRole);
