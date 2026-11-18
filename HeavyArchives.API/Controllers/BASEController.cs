using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BASEController<T>: ControllerBase
    {
        protected readonly ILogger<T> logger;

        protected BASEController(ILogger<T>logger)
        {
            this.logger = logger;
        }
        protected void LogInfo(string info)
        {
            String IPAdress = GetLocalIDAdress();
            String route = Request.Path.Value??"Unknown";
            logger.LogInformation($"[{IPAdress}] -> {info} | {route}");
        }

        private static string GetLocalIDAdress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipAdress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            if (ipAdress == null) { throw new InvalidIPAdress(); }
            return ipAdress.ToString();
        }
    }
}
