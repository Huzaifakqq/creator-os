using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CourseEnrollment
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid CourseId { get; set; }

    public Guid StudentId { get; set; }

    public Guid? OrderId { get; set; }

    public string Status { get; set; } = null!;

    public decimal ProgressPercent { get; set; }

    public Guid? LastAccessedLessonId { get; set; }

    public DateTime? LastAccessedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? CertificateIssuedAt { get; set; }

    public string? CertificateUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

    public virtual User Student { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
