using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CommunityComment
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid PostId { get; set; }

    public Guid? ParentCommentId { get; set; }

    public Guid AuthorId { get; set; }

    public string Content { get; set; } = null!;

    public string? MediaUrls { get; set; }

    public int LikeCount { get; set; }

    public bool IsEdited { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual CommunityPost Post { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
