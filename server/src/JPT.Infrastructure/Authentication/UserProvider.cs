using JPT.UseCases.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace JPT.Infrastructure.Authentication;

public sealed class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

}