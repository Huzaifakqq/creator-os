using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class UserMfaSetting
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public bool IsMfaEnabled { get; set; }

    public string MfaType { get; set; } = null!;

    public string? SecretKey { get; set; }

    public string? BackupCodes { get; set; }

    public string? RecoveryEmail { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
