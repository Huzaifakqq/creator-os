using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class LessonProgress
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid EnrollmentId { get; set; }

    public Guid LessonId { get; set; }

    public string Status { get; set; } = null!;

    public decimal? Score { get; set; }

    public int TimeSpentSeconds { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CourseEnrollment Enrollment { get; set; } = null!;

    public virtual CourseLesson Lesson { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
