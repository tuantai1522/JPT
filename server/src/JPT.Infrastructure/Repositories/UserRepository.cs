using System.Linq.Expressions;
using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace JPT.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<User?> FindUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _context.Set<User>().AddAsync(user, cancellationToken);
        
        return result.Entity;
    }

    public async Task<bool> VerifyExistedEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken, params Expression<Func<User, object?>>[]? includeProperties)
    {
        var query = _context.Set<User>().AsSplitQuery().Where(x => x.Id == userId);

        // Apply the include logic dynamically using the provided Func
        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}