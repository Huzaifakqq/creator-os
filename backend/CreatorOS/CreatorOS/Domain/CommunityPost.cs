using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CommunityPost
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid SpaceId { get; set; }

    public Guid AuthorId { get; set; }

    public string PostType { get; set; } = null!;

    public string? Title { get; set; }

    public string Content { get; set; } = null!;

    public string? MediaUrls { get; set; }

    public bool IsPinned { get; set; }

    public bool IsAnnouncement { get; set; }

    public int LikeCount { get; set; }

    public int CommentCount { get; set; }

    public int ViewCount { get; set; }

    public bool IsEdited { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<CommunityComment> CommunityComments { get; set; } = new List<CommunityComment>();

    public virtual CommunitySpace Space { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
