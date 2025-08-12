using System.Linq.Expressions;
using JPT.Core.Common;
using JPT.Core.Features.Users;

namespace JPT.Core.Features.Files;

public interface IFileRepository : IRepository<User>
{
    Task<File> AddFileAsync(File file, CancellationToken cancellationToken);
    
    Task<File?> GetFileByIdAsync(Guid fileId, CancellationToken cancellationToken, params Expression<Func<File, object?>>[]? includeProperties);
    
}