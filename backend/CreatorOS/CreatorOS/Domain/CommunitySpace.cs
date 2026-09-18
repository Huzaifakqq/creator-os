using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CommunitySpace
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string SpaceType { get; set; } = null!;

    public string? IconUrl { get; set; }

    public Guid? RequiredTierId { get; set; }

    public int MemberCount { get; set; }

    public int PostCount { get; set; }

    public bool IsPublic { get; set; }

    public bool IsActive { get; set; }

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();

    public virtual MembershipTier? RequiredTier { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
