using System.Collections;
using EventPlus.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventPlus.Api.Filters;

public class UnifiedResponseFilter: IActionFilter
{
	public void OnActionExecuted(ActionExecutedContext context)
	{

		if (context.Result is EmptyResult)
		{
			var emptyResponse = new Response();
		
			context.Result = new ObjectResult(emptyResponse);

			return;
		}
		
		if (context.Result is not ObjectResult objectResult) return;

        if (objectResult.Value is null)
        {
	        var nullResponse = new Response<object?>(null);
            
            context.Result = new ObjectResult(nullResponse);

            return;
        }
        
		// Determine the response class type based on the returned value
		var responseType = objectResult.Value!.GetType();
		var responseTypeInterfaces = responseType.GetInterfaces();
		
		if (responseTypeInterfaces.Contains(typeof(IEnumerable)) && responseType != typeof(string))
        {
			var responseArray = (objectResult.Value as IEnumerable)!.Cast<object>().ToArray();

			var unifiedEnumerableResponse = new CollectionResponse<object[]>(responseArray);

			context.Result = new ObjectResult(unifiedEnumerableResponse);

			return;
		}

		var objectResponse = objectResult.Value;

		var unifiedObjectResponse = new Response<object>(objectResponse);
		
		context.Result = new ObjectResult(unifiedObjectResponse);
	}
	
	public void OnActionExecuting(ActionExecutingContext context){}
}