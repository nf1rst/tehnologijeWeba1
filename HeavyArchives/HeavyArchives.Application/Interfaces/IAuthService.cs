using HeavyArchives.Application.DTOs.Auth;

namespace HeavyArchives.Application.Interfaces
{
    public interface IAuthService
    {
        UserDTO Register(RegisterDTO registerDTO);
        AuthResponseDTO Login(LoginDTO loginDTO);
        AuthResponseDTO RefreshToken(RefreshTokenDTO refreshDTO);
        void RevokeRefreshToken(string refreshToken);
    }
}
