using JPT.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Abstractions.Data;

public interface IApplicationDbContext
{
    /// <summary>
    /// Gets the database set for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <returns>The database set for the specified entity type.</returns>
    DbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity;
}