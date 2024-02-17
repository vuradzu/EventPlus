using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventPlus.Api.Filters;

public class ArrayCountFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not ObjectResult objectResult) return;

        var responseType = objectResult.Value!.GetType();
        var responseTypeInterfaces = responseType.GetInterfaces();

        if (responseTypeInterfaces.Contains(typeof(IEnumerable)) && responseType != typeof(string))
        {
            var responseArray = (objectResult.Value as IEnumerable)!.Cast<object>().ToArray();
            context.HttpContext.Response.Headers.Append("count", responseArray.Length.ToString());
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}