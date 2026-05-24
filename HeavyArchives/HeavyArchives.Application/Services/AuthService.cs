using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork uow;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthService(IUnitOfWork uow, IPasswordHasher passwordHasher, ITokenService tokenService, IMapper mapper)
        {
            this.uow = uow;
            this.tokenService = tokenService;
            this.passwordHasher= passwordHasher;
            this.mapper = mapper;
        }

        private const int DefaultUserRoleId = 2; // 1 admin, 2 je user

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            if (await uow.Users.UsernameExistsAsync(registerDTO.Username)) throw new UserAlreadyExistsException(registerDTO.Username);
            var(hash, salt)= passwordHasher.HashPassword(registerDTO.Password);
           
            var user = new User {
                Username = registerDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt=DateTime.UtcNow,
                RoleID=DefaultUserRoleId //user 
            };

            await uow.Users.InsertAsync(user);
            await uow.SaveChangesAsync();
             
            var savedUser = uow.Users.GetUserWithDetailsByUsernameAsync(user.Username);

            return mapper.Map<UserDTO>(savedUser);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await uow.Users.GetUserWithDetailsByUsernameAsync(loginDTO.Username);
            if (user is null) throw new Exceptions.AuthenticationInvalidException("User does not exist");

            if (!passwordHasher.VerifyPassword(loginDTO.Password, user.PasswordHash, user.PasswordSalt)) throw new AuthenticationInvalidException("Incorrect login data");

            var (accessToken, accessExpiry) = tokenService.GenerateAccessToken(user); //ovaj je kraci i sluzi za pristup zasticenim resursima 
            var (refreshToken, refreshExpiry) = tokenService.GenerateRefreshToken(); // duze traje, sluzi za generiranje novi access tokena bez ponovne autenmtikacije

            user.RefreshToken= refreshToken;
            user.RefreshTokenExpiryDate= refreshExpiry;

            await uow.Users.UpdateAsync(user);
            await uow.SaveChangesAsync();
            return new AuthResponseDTO(accessToken, refreshToken, accessExpiry,refreshExpiry); 
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenDTO refreshDTO)
        {
            //validiraj istekli access token
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(refreshDTO.AccessToken);
            if (principal is null) throw new Exceptions.InvalidRefreshTokenException();

            //sdohvati user id iz tokena
            string? userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) throw new InvalidRefreshTokenException();

            //pronadi korisnika
            var user =await uow.Users.GetUserWithDetailsByIdAsync(userId);
            if(user is null ||user.RefreshToken!=refreshDTO.RefreshToken || user.RefreshTokenExpiryDate<=DateTime.UtcNow)
            {
                throw new InvalidRefreshTokenException();
            }

            var (accessToken, accessExpiry) = tokenService.GenerateAccessToken(user);
            var (refreshToken, refreshExpiry) = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = refreshExpiry;

            await uow.Users.UpdateAsync(user);
            await uow.SaveChangesAsync();

            return new AuthResponseDTO(accessToken, refreshToken, accessExpiry, refreshExpiry);
        }

        //logout
        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var user = await uow.Users.GetUserWithDetailsByRefreshTokenAsync(refreshToken);
            if (user is null) return;

            user.RefreshToken = null;
            user.RefreshTokenExpiryDate = null;
            await uow.Users.UpdateAsync(user);
             await uow.SaveChangesAsync();
        }
    }
}
