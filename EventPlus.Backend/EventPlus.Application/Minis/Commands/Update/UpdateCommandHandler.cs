using EventPlus.Application.Minis.Base;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Update;

public class UpdateCommandHandler(IServiceProvider serviceProvider)
    : MinisHandler<UpdateCommandRequest>(serviceProvider)
{
    protected override async Task Process(UpdateCommandRequest request, CancellationToken ct)
    {
        var command = await Database.Set<Command>()
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(ct);
        
        if (command is null) throw new NotFoundException("No such Command");

        command.Name = request.Name;
        command.Description = request.Description;
        
        await Database.SaveChangesAsync(ct);
    }
}