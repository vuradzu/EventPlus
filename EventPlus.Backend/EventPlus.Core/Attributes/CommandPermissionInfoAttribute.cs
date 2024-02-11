namespace EventPlus.Core.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class CommandPermissionInfoAttribute(string key, string displayName) : Attribute
{
    public string Key { get; set; } = key;
    public string DisplayName { get; set; } = displayName;
}