using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EventPlus.Application.Options;

public class S3Options
{
    public required string AccessKey { get; set; } = default!;
    public required string SecretKey { get; set; } = default!;
    public required string Service { get; set; } = default!;
    public required string Region { get; set; } = default!;

    public S3BucketNames Buckets { get; set; } = default!;


    internal sealed class Configurator(IConfiguration configuration) : IConfigureOptions<S3Options>
    {
        public void Configure(S3Options options)
        {
            var config = configuration.GetRequiredSection("AWS:S3");
            config.Bind(options);
        }
    }
}

public class S3BucketNames
{
    public string UsersBucketName { get; set; }
    public string CommandsBucketName { get; set; }
}