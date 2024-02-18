using System.Net;
using NeerCore.Exceptions;

namespace EventPlus.Core.Exceptions;

public class CustomHttpException(string message, HttpStatusCode statusCode, string errorType) : HttpException(message)
{
    public override HttpStatusCode StatusCode => statusCode;
    public override string ErrorType => errorType;
}