using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class PlatformSetting
{
    public Guid Id { get; set; }

    public string SettingKey { get; set; } = null!;

    public string? SettingValue { get; set; }

    public string? Description { get; set; }

    public bool IsEncrypted { get; set; }

    public DateTime UpdatedAt { get; set; }
}
