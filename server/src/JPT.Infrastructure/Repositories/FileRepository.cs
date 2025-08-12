using System.Linq.Expressions;
using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using File = JPT.Core.Features.Files.File;

namespace JPT.Infrastructure.Repositories;

public sealed class FileRepository(ApplicationDbContext context) : IFileRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<File> AddFileAsync(File file, CancellationToken cancellationToken)
    {
        var result = await _context.Files.AddAsync(file, cancellationToken);
        
        return result.Entity;
        
    }

    public async Task<File?> GetFileByIdAsync(Guid fileId, CancellationToken cancellationToken, params Expression<Func<File, object?>>[]? includeProperties)
    {
        var query = _context.Files.AsSplitQuery().Where(x => x.Id == fileId);

        // Apply the include logic dynamically using the provided Func
        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<File>> GetFilesByIdAsync(IReadOnlyList<Guid> fileIds, CancellationToken cancellationToken)
    {
        return await _context.Files
            .AsNoTracking()
            .Where(file => fileIds.Contains(file.Id))
            .ToListAsync(cancellationToken);
    }
}