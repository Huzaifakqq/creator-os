using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CourseQuiz
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid LessonId { get; set; }

    public string Title { get; set; } = null!;

    public string Questions { get; set; } = null!;

    public decimal PassingScore { get; set; }

    public int MaxAttempts { get; set; }

    public int? TimeLimitMinutes { get; set; }

    public bool ShuffleQuestions { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CourseLesson Lesson { get; set; } = null!;

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual Tenant Tenant { get; set; } = null!;
}
