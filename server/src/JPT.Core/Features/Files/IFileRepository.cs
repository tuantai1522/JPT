using System.Linq.Expressions;

namespace JPT.Core.Features.Files;

public interface IFileRepository
{
    Task<File> AddFileAsync(File file, CancellationToken cancellationToken);
    
    Task<File?> GetFileByIdAsync(Guid fileId, CancellationToken cancellationToken, params Expression<Func<File, object?>>[]? includeProperties);
    
    Task<IReadOnlyList<File>> GetFilesByIdAsync(IReadOnlyList<Guid> fileIds, CancellationToken cancellationToken);
    
}