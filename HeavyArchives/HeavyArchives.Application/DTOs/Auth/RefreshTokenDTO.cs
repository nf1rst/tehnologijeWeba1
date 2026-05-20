using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Auth
{
    public class RefreshTokenDTO
    {
        public string AccessToken{ get; set; }
        public string RefreshToken{ get; set; }
    }
}
