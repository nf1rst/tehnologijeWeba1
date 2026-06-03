using Exceptions;
using HeavyArchives.API.Middleware;
using System.Net;
using System.Text.Json;
namespace HeavyArchives.API.Middleware

{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next; //iduci middlewware u lancu
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware>logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) //konvencija
        {
            try
            {
                await next(context); //ako ovo nema await i ako nije async metoda ovaa i ovaj ispod, nece radit
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            logger.LogError(ex, "Unexpected error occured: {Message}", ex.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            var (statusCode, message) = ex switch
            {
                AuthenticationInvalidException => (HttpStatusCode.Unauthorized, ex.Message),
                InvalidRefreshTokenException => (HttpStatusCode.Unauthorized, ex.Message),
                UserAlreadyExistsException => (HttpStatusCode.Conflict, ex.Message),
                EntityNotExistant => (HttpStatusCode.NotFound, ex.Message),
                InvalidInputException => (HttpStatusCode.BadRequest, ex.Message),
                RelationExistsException => (HttpStatusCode.Conflict, ex.Message),
                InvalidLogicException => (HttpStatusCode.BadRequest, ex.Message),
                NoRelationException => (HttpStatusCode.NotFound, ex.Message),
                _ => (HttpStatusCode.InternalServerError, ex.Message)
            };

            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(
                new
                {
                    error = message, // ovdje bi se moglo za svaki dat neki custom message - HttpStatusCode.NotFound => "custom messagee",...
                    statusCode = (int)statusCode,
                });
            await response.WriteAsync(result);
        }
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    //registriranje middleware, extenzijska metoda
    //zasto ovo ne u posebnu klasu ili u vec postojece ektenzije
    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}