using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Invite.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Commands.Invite;

public record CreateInviteRequest([FromRoute] long CommandId): IMinisRequest<InviteCodeModel>;