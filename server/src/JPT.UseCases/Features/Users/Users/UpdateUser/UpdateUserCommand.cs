using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.UpdateUser;

public sealed record UpdateUserCommand(
    string FirstName,
    string? MiddleName,
    string? LastName,
    Guid? AvatarId, 
    string? Description,
    string CompanyName,
    string? CompanyDescription,
    Guid? LogoId) : IRequest<Result<Guid>>;
