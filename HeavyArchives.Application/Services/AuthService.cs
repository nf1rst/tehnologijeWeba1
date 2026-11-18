using AutoMapper;
using EncyclopaediaBisemutum.Application.DTOs.Auth;
using Exceptions;
using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        private const int DefaultUserRoleId = 2;

        public async Task<UserDTO> RegisterAsync(UserRegisterWithDetailsDTO registerDTO)
        {
            if (await uow.Users.UsernameExistsAsync(registerDTO.Username)) throw new UserAlreadyExistsException(registerDTO.Username);
            var(hash, salt)= passwordHasher.HashPassword(registerDTO.Password);
           
            User user = new User {
                Username = registerDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt=DateTime.UtcNow,
                RoleID=DefaultUserRoleId, //user
                FavoriteBand=registerDTO.FavoriteBand
            };

            await uow.Users.InsertAsync(user);
            await uow.SaveChangesAsync();
             
            var savedUser =await uow.Users.GetUserWithDetailsByUsernameAsync(user.Username);

            return mapper.Map<UserDTO>(savedUser);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await uow.Users.GetUserWithDetailsByUsernameAsync(loginDTO.Username);
            if (user is null) throw new Exceptions.AuthenticationInvalidException("User does not exist");

            if (!passwordHasher.VerifyPassword(loginDTO.Password, user.PasswordHash, user.PasswordSalt)) throw new AuthenticationInvalidException("Incorrect login data");

            var (accessToken, accessExpiry) = tokenService.GenerateAccessToken(user); 
            var (refreshToken, refreshExpiry) = tokenService.GenerateRefreshToken(); 

            user.RefreshToken= refreshToken;
            user.RefreshTokenExpiryDate= refreshExpiry;

            await uow.Users.UpdateAsync(user);
            await uow.SaveChangesAsync();
            return new AuthResponseDTO(accessToken, refreshToken, accessExpiry,refreshExpiry); 
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenDTO refreshDTO)
        {
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(refreshDTO.AccessToken);
            if (principal is null) throw new Exceptions.InvalidRefreshTokenException("No claims in access token");

            string? userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) throw new InvalidRefreshTokenException("User information in claims invalid");

            var user =await uow.Users.GetUserWithDetailsByIdAsync(userId);
            if(user is null ||user.RefreshToken!=refreshDTO.RefreshToken || user.RefreshTokenExpiryDate<=DateTime.UtcNow)
            {
                throw new InvalidRefreshTokenException("Refresh token has expired");
            }

            var (accessToken, accessExpiry) = tokenService.GenerateAccessToken(user);
            var (refreshToken, refreshExpiry) = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = refreshExpiry;

            await uow.Users.UpdateAsync(user);
            await uow.SaveChangesAsync();

            return new AuthResponseDTO(accessToken, refreshToken, accessExpiry, refreshExpiry);
        }
        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var user = await uow.Users.GetUserWithDetailsByRefreshTokenAsync(refreshToken);
            if (user is null) return;

            user.RefreshToken = null;
            user.RefreshTokenExpiryDate = null;
            await uow.Users.UpdateAsync(user);
             await uow.SaveChangesAsync();
        }

        public async Task<UserDTO> ChangePassowrd(string accessToken, string oldPassword, string newPassword)
        {
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal is null) throw new InvalidRefreshTokenException("No claims pincipal in access token");

            string? userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) throw new InvalidRefreshTokenException("User information in claims invalid");

            User? user = await uow.Users.GetUserWithDetailsByIdAsync(userId);
            if (user is null) throw new EntityNotExistant("User not found");

            if (!passwordHasher.VerifyPassword(oldPassword, user.PasswordHash, user.PasswordSalt)) throw new InvalidInputException("Password incorrect");

            var (password, salt) = passwordHasher.HashPassword(newPassword);
            
            user.PasswordHash = password;
            user.PasswordSalt = salt;

            await uow.SaveChangesAsync();
            return mapper.Map<UserDTO>(user);
        }
    }
}
