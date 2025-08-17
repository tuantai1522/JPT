namespace JPT.UseCases.Features.Users.Users.LogIn;

public sealed record LogInResponse(string AccessToken, string FirstName, string Email, string UserRole);
