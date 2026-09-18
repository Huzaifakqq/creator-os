using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CrmContact
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string ContactType { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Company { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Source { get; set; }

    public string Status { get; set; } = null!;

    public int Score { get; set; }

    public long LifetimeValueCents { get; set; }

    public DateTime? LastContactedAt { get; set; }

    public DateTime? ConvertedAt { get; set; }

    public string? CustomFields { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CrmActivity> CrmActivities { get; set; } = new List<CrmActivity>();

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual ICollection<EmailSequenceEnrollment> EmailSequenceEnrollments { get; set; } = new List<EmailSequenceEnrollment>();

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<CrmTag> Tags { get; set; } = new List<CrmTag>();
}
