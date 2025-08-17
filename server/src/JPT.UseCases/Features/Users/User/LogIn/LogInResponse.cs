namespace JPT.UseCases.Features.Users.User.LogIn;

public sealed record LogInResponse(string AccessToken, string FirstName, string Email, string UserRole);
