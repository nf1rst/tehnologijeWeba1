using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Auth
{
    public record AuthResponseDTO(
        string AccessToken,
        string RefreshToken,
        DateTime AccesTokenExpiry,
        DateTime RefreshTokenExpiry
        );
}
