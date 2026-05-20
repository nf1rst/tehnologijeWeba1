using HeavyArchives.Domain.Entities;
using System.Security.Claims;

namespace HeavyArchives.Application.Interfaces
{
    public interface ITokenService
    {
        //tuple 
        (string token, DateTime expiry) GenerateAccessToken(User user);
        (string token, DateTime expiry) GenerateRefreshToken();
        /// <summary>
        /// dohvaca claims principal iz isteklom access tokena pa se koristi za refresh token flow
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
