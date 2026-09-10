namespace CreatorOS.Domain.Common.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

public interface ITenantRepository<T> : IRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllByTenantAsync(Guid tenantId);
    Task<T?> GetByIdAndTenantAsync(Guid id, Guid tenantId);
}

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
