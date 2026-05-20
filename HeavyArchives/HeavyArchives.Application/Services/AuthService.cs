using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        public UserDTO Register(RegisterDTO registerDTO)
        {
            if (uow.Users.UsernameExists(registerDTO.Username)) throw new UserAlreadyExistsException(registerDTO.Username);
            var(hash, salt)= passwordHasher.HashPassword(registerDTO.Password);
           
            var user = new User {
                Username = registerDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt=DateTime.UtcNow,
                RoleID=DefaultUserRoleId //user 
            };

            uow.Users.Insert(user);
            uow.SaveChanges();

            var savedUser = uow.Users.GetUserWithDetailsByUsername(user.Username);

            return mapper.Map<UserDTO>(savedUser);
        }

        public AuthResponseDTO Login(LoginDTO loginDTO)
        {
            var user = uow.Users.GetUserWithDetailsByUsername(loginDTO.Username);
            if (user is null) throw new Exceptions.AuthenticationInvalidException("User does not exist");

            if (!passwordHasher.VerifyPassword(loginDTO.Password, user.PasswordHash, user.PasswordSalt)) throw new AuthenticationInvalidException("Incorrect login data");

            var (accessToken, accessExpiry) = tokenService.GenerateAccessToken(user); //ovaj je kraci i sluzi za pristup zasticenim resursima 
            var (refreshToken, refreshExpiry) = tokenService.GenerateRefreshToken(); // duze traje, sluzi za generiranje novi access tokena bez ponovne autenmtikacije

            user.RefreshToken= refreshToken;
            user.RefreshTokenExpiryDate= refreshExpiry;

            uow.Users.Update(user);
            uow.SaveChanges();
            return new AuthResponseDTO(accessToken, refreshToken, accessExpiry,refreshExpiry); 
        }

        public AuthResponseDTO RefreshToken(RefreshTokenDTO refreshDTO)
        {
            //validiraj istekli access token
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(refreshDTO.AccessToken);
            if (principal is null) throw new Exceptions.InvalidRefreshTokenException();

            //sdohvati user id iz tokena
            string? userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) throw new InvalidRefreshTokenException();

            //pronadi korisnika
            var user = uow.Users.GetUserWithDetailsById(userId);
            if(user is null ||user.RefreshToken!=refreshDTO.RefreshToken || user.RefreshTokenExpiryDate<=DateTime.UtcNow)
            {
                throw new InvalidRefreshTokenException();
            }

            var (accessToken, accessExpiry) = tokenService.GenerateAccessToken(user);
            var (refreshToken, refreshExpiry) = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = refreshExpiry;

            uow.Users.Update(user);
            uow.SaveChanges();

            return new AuthResponseDTO(accessToken, refreshToken, accessExpiry, refreshExpiry);
        }

        //logout
        public void RevokeRefreshToken(string refreshToken)
        {
            var user = uow.Users.GetUserWithDetailsByRefreshToken(refreshToken);
            if (user is null) return;

            user.RefreshToken = null;
            user.RefreshTokenExpiryDate = null;
            uow.Users.Update(user);
            uow.SaveChanges();
        }
    }
}
