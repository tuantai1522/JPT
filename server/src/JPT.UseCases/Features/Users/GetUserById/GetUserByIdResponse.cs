namespace JPT.UseCases.Features.Users.GetUserById;

public sealed record GetUserByIdResponse(
    string FirstName,
    string? MiddleName,
    string? LastName,
    string? Description,
    string? AvatarUrl,
    string Role,
    string? CompanyName,
    string? CompanyDescription,
    string? LogoUrl,
    IReadOnlyList<string> Cvs);
