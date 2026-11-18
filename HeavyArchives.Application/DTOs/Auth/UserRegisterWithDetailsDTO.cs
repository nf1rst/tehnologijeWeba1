using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.DTOs.Auth
{
    public class UserRegisterWithDetailsDTO
    {
        public string Username{ get; set; }
        public string Password{ get; set; }
        public int Age{ get; set; }
        public string FavoriteBand{ get; set; }
    }
}
