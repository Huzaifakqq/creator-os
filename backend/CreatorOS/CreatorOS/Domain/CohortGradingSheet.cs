using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CohortGradingSheet
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid CourseId { get; set; }

    public Guid InstructorId { get; set; }

    public string Title { get; set; } = null!;

    public string? RubricConfig { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
