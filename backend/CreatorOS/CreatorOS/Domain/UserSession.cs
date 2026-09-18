using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class UserSession
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? DeviceInfo { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime LastActiveAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
