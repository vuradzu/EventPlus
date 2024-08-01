using EventPlus.Application.Hangfire;
using Hangfire;

namespace EventPlus.Infrastructure.Services.Jobs;

public class FakeJob : IJob
{
    public void RunJob()
    {
        // RecurringJob.AddOrUpdate<FakeJobService>(
        //     "fake",
        //     service => service.Process(),
        //     Cron.Hourly
        // );
    }
}

public class FakeJobService : IJobService
{
    public async Task Process()
    {
        Console.WriteLine("Fake");
    }
}