using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class DigitalAsset
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ProductId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public string? FileType { get; set; }

    public long FileSizeBytes { get; set; }

    public int DownloadCount { get; set; }

    public int? MaxDownloads { get; set; }

    public string AccessLevel { get; set; } = null!;

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
