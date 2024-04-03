using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Stock.Exceptions;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { 
        
        if (!context.ModelState.IsValid)
        {
            var errors = string.Join(", ", context.ModelState.Values.SelectMany(value => value.Errors).Select(error => error.ErrorMessage));
            var Response = new { message = "Validation Error - "+errors };
            context.Result = new ObjectResult(Response)
            {
                StatusCode = 400
            };
        }

    }
    public void OnActionExecuted(ActionExecutedContext context) {}
}