using EventPlus.Application.Hangfire;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Infrastructure.Services.Jobs;

public class DeleteOldEventJob : IJob
{
    public void RunJob()
    {
        RecurringJob.AddOrUpdate<DeleteOldEventJobService>(
            "event",
            service => service.Process(),
            Cron.Monthly
        );
    }
}

public class DeleteOldEventJobService(ISqlServerDatabase database, CancellationToken ct) : IJobService
{
    public async Task Process()
    {
        var events = await database.Set<Event>()
            .IgnoreQueryFilters()
            .Where(e => e.Deleted != null)
            .ToArrayAsync();

        var eventsDelete = events.Where(e => e.Deleted <= DateTime.Now.AddDays(-31)).ToArray();

        database.RemoveRange(eventsDelete);
        await database.SaveChangesAsync(ct);
    }
}