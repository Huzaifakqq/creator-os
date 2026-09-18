using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Course
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? ProductId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string Difficulty { get; set; } = null!;

    public decimal? EstimatedHours { get; set; }

    public int EnrollmentCount { get; set; }

    public decimal? CompletionRate { get; set; }

    public decimal? AverageRating { get; set; }

    public bool IsPublished { get; set; }

    public bool CertificateEnabled { get; set; }

    public string? CertificateTemplate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CohortGradingSheet> CohortGradingSheets { get; set; } = new List<CohortGradingSheet>();

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

    public virtual Tenant Tenant { get; set; } = null!;
}
