using JPT.Core.Common;

namespace JPT.Core.Features.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
    
    Task<bool> VerifyExistedEmailAsync(string email, CancellationToken cancellationToken);
}