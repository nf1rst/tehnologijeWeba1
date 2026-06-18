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
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
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
