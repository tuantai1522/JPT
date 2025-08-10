using JPT.Core.Common;
using JPT.Core.Features.Users;

namespace JPT.Core.Features.Files;

public interface IFileRepository : IRepository<User>
{
    Task<File> AddFileAsync(File file, CancellationToken cancellationToken);
    
}