using HeavyArchives.Application.Interfaces;
using HeavyArchives.Application.Options;
using HeavyArchives.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HeavyArchives.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings jwtSettings;
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }
        public (string token, DateTime expiry) GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)); //key za potpisivanje tokena 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // credentials - kako se token opisuje

            var expiry = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes); // koliko token vrijedi 

            var claims = new List<Claim> //podaci unutar tokena 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //subject - 
                new Claim (JwtRegisteredClaimNames.Email, user.Username),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT ID - da se osigura da se token moze koristit samo jedanput i da je unikatan
                new Claim (JwtRegisteredClaimNames.Iat, // Issued at - vrijeme kad je JWT izdan 
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };  


            //u primjeru on u useru ima listu userRoles, ali to meni nema smisla, pa jel onda ovo dobro promijenjen kod
            if (user.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name)); // ovo sluzi za ono u kotrnoleru - [Authorize(Roles="Admin")]
            }

            var token = new JwtSecurityToken( // kreiranje tokena s parametrima koji su tamo u appsettingsjson
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expiry,
                signingCredentials: credentials
            );
            return (new JwtSecurityTokenHandler().WriteToken(token), expiry);
        }

        public (string token, DateTime expiry) GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var expiry = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpirationDays);
            return (Convert.ToBase64String(randomBytes), expiry);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, //dozvoljava da se moze procitat istekli token
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal? principal = tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out var securityToken);

                if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                { // provjera jel token validan
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
