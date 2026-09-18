using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AssignmentSubmission
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AssignmentId { get; set; }

    public Guid StudentId { get; set; }

    public string? SubmissionData { get; set; }

    public string Status { get; set; } = null!;

    public decimal? Score { get; set; }

    public string? Feedback { get; set; }

    public Guid? GradedBy { get; set; }

    public DateTime? GradedAt { get; set; }

    public DateTime SubmittedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CourseAssignment Assignment { get; set; } = null!;

    public virtual User Student { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
