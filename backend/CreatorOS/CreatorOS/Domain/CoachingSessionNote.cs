using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CoachingSessionNote
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid BookingId { get; set; }

    public Guid AuthorId { get; set; }

    public string NoteType { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Attachments { get; set; }

    public bool IsPrivate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CoachingBooking Booking { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
