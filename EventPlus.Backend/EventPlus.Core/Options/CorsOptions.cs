namespace EventPlus.Core.Options;

public sealed class CorsOptions
{
    public string[] AllowedOrigins { get; set; } = default!;
}