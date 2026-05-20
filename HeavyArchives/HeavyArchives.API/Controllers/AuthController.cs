using HeavyArchives.Application.DTOs.Auth;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        //[ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var user = authService.Register(registerDTO);
            return CreatedAtAction(nameof(Register), user);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var response = authService.Login(loginDTO);
            return Ok(response);
        }
        
        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenDTO refreshDTO)
        {
            var response = authService.RefreshToken(refreshDTO);
            return Ok(response);
        }
        [HttpPost("logout")]
        public IActionResult Logout(RefreshTokenDTO refreshDTO)
        {
            authService.RevokeRefreshToken(refreshDTO.RefreshToken);
            return NoContent();
        }
    }
}
