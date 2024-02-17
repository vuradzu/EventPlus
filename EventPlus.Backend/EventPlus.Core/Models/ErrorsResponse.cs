namespace EventPlus.Core.Models;

public class ErrorsResponse(ICollection<string> errors, string type)
{
    public string Type { get; set; } = type;
    public ICollection<string> Errors { get; set; } = errors;
    public ICollection<string>? Trace { get; set; }

    public static ErrorsResponse CreateError(string[] messages,
        string type = "InternalServerError", bool useTrace = false, Exception? innerException = null)
    {
        var response = new ErrorsResponse(messages, type);

        if (useTrace)
        {
            response.Trace = (innerException?.StackTrace
                              ?? innerException?.ToString())?
                .Split('\n')
                .Select(x => x.Trim()).ToArray() ?? [];
        }

        return response;
    }
}