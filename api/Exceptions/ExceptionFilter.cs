using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Stock.Exceptions;

public class ExceptionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            if (context.Exception is ControllerException exception)
            {
                var Response = new { message = exception.Message };
                context.Result = new ObjectResult(Response)
                {
                    StatusCode = exception.StatusCode
                };
                
            }
            else {
                var Response = new { message = "Internal error" };
                context.Result = new ObjectResult(Response)
                {
                    StatusCode = 500
                };
            }
            context.ExceptionHandled = true;
        }
    }
}