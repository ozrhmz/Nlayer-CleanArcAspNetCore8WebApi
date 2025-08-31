using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AppClean.API.ExceptionHandler;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CriticalException)
        {
            Console.WriteLine("CriticalExceptionHandler Hatası Mevcut");
        }

        return ValueTask.FromResult(false);
    }
}

