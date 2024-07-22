using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EventPlus.Application.Options;

public class GoogleOptions
{
    public required string ClientId { get; set; } = default!;
    public required string ClientSecret { get; set; } = default!;

    internal sealed class Configurator(IConfiguration configuration) : IConfigureOptions<GoogleOptions>
    {
        public void Configure(GoogleOptions options)
        {
            var config = configuration.GetRequiredSection("Google");
            config.Bind(options);
        }
    }
}