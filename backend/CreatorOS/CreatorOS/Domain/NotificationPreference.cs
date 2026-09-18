using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class NotificationPreference
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public bool EmailEnabled { get; set; }

    public bool PushEnabled { get; set; }

    public bool InAppEnabled { get; set; }

    public bool OrderUpdates { get; set; }

    public bool SubscriptionUpdates { get; set; }

    public bool AgentActivity { get; set; }

    public bool Marketing { get; set; }

    public bool CommunityUpdates { get; set; }

    public bool WeeklyDigest { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
