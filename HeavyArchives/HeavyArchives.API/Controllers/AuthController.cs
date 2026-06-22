using EncyclopaediaBisemutum.Application.DTOs.Auth;
using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.Interfaces;
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
        public AuthController(IAuthService authService, IValidator<UserRegisterWithDetailsDTO> validator)
        {
            this.authService = authService;
            this.validator = validator;
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
            return CreatedAtAction(nameof(Register), user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response =await authService.LoginAsync(loginDTO);
            return Ok(response);
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshDTO)
        {
            var response =await authService.RefreshTokenAsync(refreshDTO);
            return Ok(response);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenDTO refreshDTO)
        {
            await authService.RevokeRefreshTokenAsync(refreshDTO.RefreshToken);
            return NoContent();
        }

    
    }
}
