using HeavyArchives.Application.Interfaces;
using HeavyArchives.Application.Options;
using HeavyArchives.Domain.Entities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;   //instead of JsonWebTokenHandler, simpler API
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HeavyArchives.Application.Services
{

    //https://youtu.be/mgeuh8k3I4g?si=akVjp-au085jTZMP + milan jovanovic
    public class TokenService : ITokenService
    {
        //https://thecodeman.net/posts/live-loading-appsettings-configuration-file
        private readonly JwtSettings jwtSettings;
        //private readonly IOptions<JwtSettings> jwtSettings;
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }

        public (string token, DateTime expiry) GenerateAccessToken(User user)
        {
            string secretKey = jwtSettings.SecretKey;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var exipiry = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name!),
                new Claim("favoriteBand", user.FavoriteBand)
            };

            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: exipiry,
                signingCredentials: credentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return (token, exipiry);
        }
        public (string token, DateTime expiry) GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var token = Convert.ToBase64String(randomBytes);
            var expiry = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpirationDays);
            return (token, expiry);
        }

        //kad token expirea treba procitat userID da se tom istom useru zna izdat novi token 
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,


                ValidateLifetime = false //isteknuti tokeni

            };

            try
            {
                //ne gleda expiry   
                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(token, validationParameters, out _);
                return principal;
            }   
            catch
            {
                //token je promijenjen 
                return null;
            }
        }
    }
}
