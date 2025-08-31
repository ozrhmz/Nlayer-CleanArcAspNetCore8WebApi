using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Services;

public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            List<string> errors = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            ServiceResult resultModel = ServiceResult.Fail(errors);
            context.Result = new BadRequestObjectResult(resultModel);
            return;
        }

        await next();
    }
}
