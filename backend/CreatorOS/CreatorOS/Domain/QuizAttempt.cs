using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class QuizAttempt
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid QuizId { get; set; }

    public Guid StudentId { get; set; }

    public string Answers { get; set; } = null!;

    public decimal Score { get; set; }

    public bool Passed { get; set; }

    public int? TimeTakenSeconds { get; set; }

    public int AttemptNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CourseQuiz Quiz { get; set; } = null!;

    public virtual User Student { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
