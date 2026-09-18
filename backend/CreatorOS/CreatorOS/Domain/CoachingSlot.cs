using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CoachingSlot
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid CoachId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int DurationMinutes { get; set; }

    public long PriceCents { get; set; }

    public string Currency { get; set; } = null!;

    public string SessionType { get; set; } = null!;

    public int MaxParticipants { get; set; }

    public string? RecurringPattern { get; set; }

    public string? AvailableDays { get; set; }

    public string? AvailableTimeSlots { get; set; }

    public string TimeZone { get; set; } = null!;

    public int BufferMinutes { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Coach { get; set; } = null!;

    public virtual ICollection<CoachingBooking> CoachingBookings { get; set; } = new List<CoachingBooking>();

    public virtual Tenant Tenant { get; set; } = null!;
}
