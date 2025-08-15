using System.Linq.Expressions;
using JPT.Core.Common;

namespace JPT.Core.Features.Users;

public interface IUserRepository
{
    Task<User?> FindUserByEmailAsync(string email, CancellationToken cancellationToken);
    
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
    
    Task<bool> VerifyExistedEmailAsync(string email, CancellationToken cancellationToken);
    
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken, params Expression<Func<User, object?>>[]? includeProperties);

}