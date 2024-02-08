using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Delete;

public class DeleteCommandHandler(IServiceProvider serviceProvider)
    : MinisHandler<DeleteCommandRequest>(serviceProvider)
{
    public override async Task Handle(DeleteCommandRequest request, CancellationToken ct)
    {
        var command = await Database.Set<Command>().FirstOrDefaultAsync(e => e.Id == request.Id, ct);
        
        if (command is null)
            throw new NotFoundException("No such command");
                
        Database.Remove(command);
        await Database.SaveChangesAsync(ct);
    }
}