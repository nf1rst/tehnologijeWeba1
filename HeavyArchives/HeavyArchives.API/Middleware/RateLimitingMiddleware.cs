using EncyclopaediaBisemutum.Domain.Entities;
using HeavyArchives.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EncyclopaediaBisemutum.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate next;
        public RateLimitingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, HeavyArchivesContext dbCotnext)
        {
            var clientAdres = httpContext.Connection.RemoteIpAddress.ToString();

            var windowStart = DateTime.UtcNow.AddMinutes(-1); // limitano za svaku minutu 

           var requestCount=await dbCotnext.Logs
                .Where(l => l.IpAdress == clientAdres && l.TimeStamp > windowStart)
                .CountAsync();

            if (requestCount >= 10)
            {
                httpContext.Response.StatusCode =(int) HttpStatusCode.TooManyRequests;
                await httpContext.Response.WriteAsync("rate limit exceeded");
                return;
            }

            Log log = new Log()
            {
                IpAdress = clientAdres,
                TimeStamp = DateTime.UtcNow
            };
            
            dbCotnext.Add(log);
            await dbCotnext.SaveChangesAsync();

            await next(httpContext);
        }

    }
}
