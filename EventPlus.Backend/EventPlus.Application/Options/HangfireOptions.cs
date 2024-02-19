using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EventPlus.Application.Options;

public class HangfireOptions
{
    public string DashboardUrl { get; set; } = default!;
    public ICollection<HangfireUser> Users { get; set; } = default!;
    
    internal sealed class Configurator(IConfiguration configuration) : IConfigureOptions<HangfireOptions>
    {
        public void Configure(HangfireOptions options)
        {
            var config = configuration.GetRequiredSection("Hangfire");
            config.Bind(options);
        }
    }
}

public class HangfireUser
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}