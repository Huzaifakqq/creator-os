using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CoachingBooking
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid SlotId { get; set; }

    public Guid ClientId { get; set; }

    public Guid? OrderId { get; set; }

    public DateTime SessionDate { get; set; }

    public string SessionTime { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public string Status { get; set; } = null!;

    public string? MeetingUrl { get; set; }

    public string? Notes { get; set; }

    public string? IntakeFormData { get; set; }

    public bool ReminderSent { get; set; }

    public string? CancellationReason { get; set; }

    public DateTime? CanceledAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Client { get; set; } = null!;

    public virtual ICollection<CoachingSessionNote> CoachingSessionNotes { get; set; } = new List<CoachingSessionNote>();

    public virtual CoachingSlot Slot { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
