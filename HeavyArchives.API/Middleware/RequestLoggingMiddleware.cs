
using Microsoft.AspNetCore.Identity.Data;

namespace EncyclopaediaBisemutum.API.Middleware
{
    public class RequestLoggingMiddleware 
    {
        private readonly ILogger<RequestLoggingMiddleware> logger;
        private readonly RequestDelegate next;

        public RequestLoggingMiddleware(RequestDelegate next,ILogger<RequestLoggingMiddleware>logger)
        {
            this.next = next;
            this.logger = logger;
        }

        //https://www.softacom.com/blog/understanding-middleware-in-asp-net-core-logging-correlation-ids-and-best-practices/
        public async Task InvokeAsync(HttpContext context)
        {
            logger.LogInformation("Incoming request: Method: {method}, Path: {Path}, Query: {query}", context.Request.Method, context.Request.Path, context.Request.Query);

            await next(context);

            logger.LogInformation("Outgoing response: StatusCode: {status}", context.Response.StatusCode);
        }
    }
}
