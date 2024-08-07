using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TodoApp.Service.DTO;

namespace TodoApp
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new Response<GlobalExceptionHandler>()
            {
                StatusCode = (int?)HttpStatusCode.InternalServerError,
                Message = exception.Message,
                IsSuccess = false
            };
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
