using CreatorOS.Domain.Common.Base;
using CreatorOS.Domain.Common.Interfaces;
using CreatorOS.Domain.Identity;
using CreatorOS.Domain.Workspace;
using CreatorOS.Domain.Commerce;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreatorOS.Infrastructure.Persistence;

public class CreatorOSDbContext : DbContext, IUnitOfWork
{
    private readonly ITenantContext _tenantContext;

    public CreatorOSDbContext(DbContextOptions<CreatorOSDbContext> options, ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    // Identity
    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<UserTenantMembership> UserTenantMemberships => Set<UserTenantMembership>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    // Workspace
    public DbSet<Domain.Workspace.Workspace> Workspaces => Set<Domain.Workspace.Workspace>();
    public DbSet<App> Apps => Set<App>();
    public DbSet<AppVersion> AppVersions => Set<AppVersion>();

    // Commerce
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreatorOSDbContext).Assembly);

        // Global query filters for tenant isolation
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(TenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(CreateTenantFilter(entityType.ClrType));
            }
        }
    }

    private static LambdaExpression CreateTenantFilter(Type entityType)
    {
        var parameter = System.Linq.Expressions.Expression.Parameter(entityType, "e");
        var tenantIdProperty = System.Linq.Expressions.Expression.Property(parameter, nameof(TenantEntity.TenantId));
        var tenantIdValue = System.Linq.Expressions.Expression.Property(
            System.Linq.Expressions.Expression.Constant(new TenantContextStub()), nameof(ITenantContext.TenantId));
        var convertedValue = System.Linq.Expressions.Expression.Convert(tenantIdValue, typeof(Guid?));
        var comparison = System.Linq.Expressions.Expression.Equal(tenantIdProperty, convertedValue);
        return System.Linq.Expressions.Expression.Lambda(comparison, parameter);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private class TenantContextStub : ITenantContext
    {
        public Guid? TenantId { get; set; }
        public void SetTenantId(Guid tenantId) => TenantId = tenantId;
    }
}
