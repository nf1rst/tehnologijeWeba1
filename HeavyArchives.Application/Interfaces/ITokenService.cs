using HeavyArchives.Domain.Entities;
using System.Security.Claims;

namespace HeavyArchives.Application.Interfaces
{
    public interface ITokenService
    {
        (string token, DateTime expiry) GenerateAccessToken(User user);
        (string token, DateTime expiry) GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
