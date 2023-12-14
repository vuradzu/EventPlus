using System.Net;
using NeerCore.Exceptions;

namespace EventPlus.Core.Exceptions;

public class PermissionsException() : HttpException("You have no permissions to do this")
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

    public override string ErrorType => "PermissionDenied";
}