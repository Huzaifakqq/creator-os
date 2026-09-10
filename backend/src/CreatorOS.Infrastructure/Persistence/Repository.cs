using CreatorOS.Domain.Common.Interfaces;
using CreatorOS.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Infrastructure.Persistence;

public class Repository<T> : ITenantRepository<T> where T : TenantEntity
{
    protected readonly CreatorOSDbContext _context;
    protected readonly ITenantContext _tenantContext;

    public Repository(CreatorOSDbContext context, ITenantContext tenantContext)
    {
        _context = context;
        _tenantContext = tenantContext;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllByTenantAsync(Guid tenantId)
    {
        return await _context.Set<T>()
            .Where(e => e.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAndTenantAsync(Guid id, Guid tenantId)
    {
        return await _context.Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId);
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.TenantId = _tenantContext.TenantId!.Value;
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}
