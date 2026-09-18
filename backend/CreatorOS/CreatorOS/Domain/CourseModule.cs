using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CourseModule
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int SortOrder { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<CourseLesson> CourseLessons { get; set; } = new List<CourseLesson>();

    public virtual Tenant Tenant { get; set; } = null!;
}
