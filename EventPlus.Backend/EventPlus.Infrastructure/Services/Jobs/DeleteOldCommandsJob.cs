using EventPlus.Application.Hangfire;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EventPlus.Infrastructure.Services.Jobs;

public class DeleteOldCommandsJob : IJob
{
    public void RunJob()
    {
        RecurringJob.AddOrUpdate<DeleteOldCommandsJobService>(
            "delete-old-commands",
            service => service.Process(),
            Cron.Monthly
        );
    }
}

public class DeleteOldCommandsJobService(ISqlServerDatabase database, CancellationToken ct) : IJobService
{
    public async Task Process()
    {
        var commands = await database.Set<Command>()
            .IgnoreQueryFilters()
            .Where(c => c.Deleted != null)
            .ToArrayAsync();

        var commandsDelete = commands.Where(c => c.Deleted.Value.AddDays(31) <= DateTime.Now).ToArray();

        database.RemoveRange(commandsDelete);
        await database.SaveChangesAsync(ct);
    }
}