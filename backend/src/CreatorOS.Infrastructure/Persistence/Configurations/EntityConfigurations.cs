using CreatorOS.Domain.Identity;
using CreatorOS.Domain.Workspace;
using CreatorOS.Domain.Commerce;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorOS.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).HasMaxLength(320).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.DisplayName).HasMaxLength(200);
    }
}

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
        builder.Property(t => t.Slug).HasMaxLength(200).IsRequired();
        builder.HasIndex(t => t.Slug).IsUnique();
    }
}

public class WorkspaceConfiguration : IEntityTypeConfiguration<Domain.Workspace.Workspace>
{
    public void Configure(EntityTypeBuilder<Domain.Workspace.Workspace> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Name).HasMaxLength(200).IsRequired();
        builder.Property(w => w.Slug).HasMaxLength(200).IsRequired();
        builder.HasIndex(w => new { w.TenantId, w.Slug }).IsUnique();
    }
}

public class AppConfiguration : IEntityTypeConfiguration<App>
{
    public void Configure(EntityTypeBuilder<App> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
        builder.Property(a => a.Slug).HasMaxLength(200).IsRequired();
        builder.HasIndex(a => new { a.TenantId, a.Slug }).IsUnique();
    }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Currency).HasMaxLength(3);
        builder.HasIndex(p => p.TenantId);
    }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasIndex(o => new { o.TenantId, o.CreatedAt });
    }
}
