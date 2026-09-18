using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CourseAssignment
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid LessonId { get; set; }

    public string Title { get; set; } = null!;

    public string? Instructions { get; set; }

    public DateTime? DueDate { get; set; }

    public decimal? MaxScore { get; set; }

    public string? SubmissionTypes { get; set; }

    public int MaxFileSizeMb { get; set; }

    public bool AllowLateSubmission { get; set; }

    public decimal? LatePenaltyPercent { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();

    public virtual CourseLesson Lesson { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
