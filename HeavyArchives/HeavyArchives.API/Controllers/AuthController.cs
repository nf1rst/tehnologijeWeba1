using EncyclopaediaBisemutum.API.Controllers;
using EncyclopaediaBisemutum.Application.DTOs.Auth;
using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IValidator<UserRegisterWithDetailsDTO> validator;
        private readonly ILogger<AuthController> logger;
        public AuthController(IAuthService authService, IValidator<UserRegisterWithDetailsDTO> validator, ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.validator = validator;
            this.logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterWithDetailsDTO registerDTO)
        {
            var res = await validator.ValidateAsync(registerDTO);
            if (!res.IsValid)
            {
                string message = string.Join(", ", res.Errors);
                throw new InvalidInputException(message);
            }
            var user =await authService.RegisterAsync(registerDTO);
            logger.LogInformation("[{Controller}] New user registered: {@User}", nameof(AuthController), user);
            return CreatedAtAction(nameof(Register), user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response =await authService.LoginAsync(loginDTO);
            logger.LogInformation("[{Controller}] Admin user logged in: {@AuthDTO}", nameof(AdminController), response);
            return Ok(response);
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshDTO)
        {
            var response =await authService.RefreshTokenAsync(refreshDTO);
            logger.LogInformation("[{Controller}] Access token refreshed: {@AuthDTO}", nameof(AuthController), response);
            return Ok(response);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenDTO refreshDTO)
        {
            await authService.RevokeRefreshTokenAsync(refreshDTO.RefreshToken);
            return NoContent();
        }

        [HttpPut("updatePassword/{accessToken}")]
        public async Task<IActionResult> ChangePassword(string accessToken, string oldPassword, string newPassword)
        {
            var user = await authService.ChangePassowrd(accessToken, oldPassword, newPassword);
            return Ok(user);
        }

    
    }
}
