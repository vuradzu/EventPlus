using System.ComponentModel.DataAnnotations;
using EventPlus.Core.Enums;
using NeerCore.Data.Abstractions;
using NeerCore.Localization;

namespace EventPlus.Domain.Entities.Identity;

public sealed class AppDevice : IEntity<long>
{
    public long Id { get; set; }

    /// <summary>
    ///   255.255.255.255 => 3*4+3 = 15 length
    ///   0000:0000:0000:0000:0000:0000:0000:0000 => 8*4+7 = 39
    ///   0000:0000:0000:0000:0000:FFFF:192.168.100.228 => (ipv6)+1+(ipv4) = 29+1+15 = 45
    /// </summary>
    public required string IpAddress { get; set; }

    /// <example>Windows 11</example>
    public required string Platform { get; init; }

    /// <example>Firefox</example>
    public required string Browser { get; init; }

    /// <example>42.0.1</example>
    public required string BrowserVersion { get; init; }

    public DeviceStatus Status { get; set; }
    public short AttemptCount { get; set; }
    public DateTimeOffset? LastAttempt { get; set; }
}