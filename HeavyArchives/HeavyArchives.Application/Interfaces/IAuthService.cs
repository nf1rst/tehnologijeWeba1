using HeavyArchives.Application.DTOs.Auth;

namespace HeavyArchives.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenDTO refreshDTO);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
