using Exceptions;
using HeavyArchives.API.Middleware;
using Microsoft.Data.SqlClient;
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
                await next(context); 
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            logger.LogError(ex, "Error occured: {Message}", ex.Message);

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
                SqlException => (HttpStatusCode.BadRequest, "Cannot execute query, FK constraints present"),
                _ => (HttpStatusCode.InternalServerError, ex.Message)
            };

            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(
                new
                {
                    error = message, 
                    statusCode = (int)statusCode,
                });
            await response.WriteAsync(result);
        }
    }
}