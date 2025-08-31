using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace App.Services.ExceptionHandlers;

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

