using JPT.Core.Common;
using JPT.Core.Features.Users;
using MediatR;

namespace JPT.UseCases.Features.Users.UpdateUser;

public sealed record UpdateUserCommand(
    string FirstName,
    string? MiddleName,
    string? LastName,
    UserRole Role,
    Guid? AvatarId, 
    string CompanyName,
    string? CompanyDescription,
    Guid? LogoId) : IRequest<Result<Guid>>;
