using EventPlus.Application.Hangfire;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Infrastructure.Services.Jobs;

public class DeleteOldEventsJob : IJob
{
    public void RunJob()
    {
        RecurringJob.AddOrUpdate<DeleteOldEventsJobService>(
            "delete-old-events",
            service => service.Process(),
            Cron.Monthly
        );
    }
}

public class DeleteOldEventsJobService(ISqlServerDatabase database, CancellationToken ct) : IJobService
{
    public async Task Process()
    {
        var events = await database.Set<Event>()
            .IgnoreQueryFilters()
            .Where(e => e.Deleted != null)
            .ToArrayAsync();

        var eventsDelete = events.Where(e => e.Deleted.Value.AddDays(31) <= DateTime.Now).ToArray();

        database.RemoveRange(eventsDelete);
        await database.SaveChangesAsync(ct);
    }
}