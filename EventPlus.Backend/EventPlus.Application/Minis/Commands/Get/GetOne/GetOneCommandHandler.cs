using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Get.GetOne;

public class GetOneCommandHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetOneCommandRequest, CommandModel>(serviceProvider)
{
    protected override async Task<CommandModel> Process(GetOneCommandRequest request, CancellationToken ct)
    {
        var command = await Database.Set<Command>()
            .Include(c => c.Events)
            .ProjectToType<CommandModel>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (command is null) throw new NotFoundException("No such Command");

        return command;
    }
}