using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CourseLesson
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ModuleId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string LessonType { get; set; } = null!;

    public string? Content { get; set; }

    public string? VideoUrl { get; set; }

    public int? VideoDurationSeconds { get; set; }

    public string? AttachmentUrls { get; set; }

    public int SortOrder { get; set; }

    public bool IsFreePreview { get; set; }

    public bool IsPublished { get; set; }

    public int? EstimatedMinutes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();

    public virtual ICollection<CourseQuiz> CourseQuizzes { get; set; } = new List<CourseQuiz>();

    public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

    public virtual CourseModule Module { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
