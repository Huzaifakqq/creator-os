using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailCampaign
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? ListId { get; set; }

    public string Name { get; set; } = null!;

    public string? Subject { get; set; }

    public string? PreviewText { get; set; }

    public string? HtmlContent { get; set; }

    public string? JsonContent { get; set; }

    public string CampaignType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? ScheduledAt { get; set; }

    public DateTime? SentAt { get; set; }

    public int TotalSent { get; set; }

    public int TotalOpened { get; set; }

    public int TotalClicked { get; set; }

    public int TotalBounced { get; set; }

    public int TotalUnsubscribed { get; set; }

    public decimal? OpenRate { get; set; }

    public decimal? ClickRate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
