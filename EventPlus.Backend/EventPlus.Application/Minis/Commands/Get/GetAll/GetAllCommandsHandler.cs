using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Commands.Get.GetAll;

public class GetAllCommandsHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetAllCommandsRequest, ICollection<CommandModel>>(serviceProvider)
{
    public override async Task<ICollection<CommandModel>> Handle(GetAllCommandsRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        
        var commands = await Database.Set<CommandMember>()
            .Where(cm => cm.AppUserId == userId)
            .Include(cm => cm.Command)
            .Select(cm => cm.Command)
            .ProjectToType<CommandModel>()
            .ToArrayAsync(ct);
        
        return commands;
    }
}