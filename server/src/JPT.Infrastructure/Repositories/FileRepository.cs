using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Infrastructure.Database;
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
}