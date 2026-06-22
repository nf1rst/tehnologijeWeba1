using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class User
    {
        public int Id{ get; set; }
        public string Username { get; set; } = String.Empty;
        public string PasswordHash{ get; set; }= String.Empty;
        public string PasswordSalt{ get; set; }=String.Empty;
        public string? RefreshToken{ get; set; }
        public DateTime? RefreshTokenExpiryDate{ get; set; }
        public DateTime CreatedAt{ get; set; }=DateTime.Now;
        public Role Role { get; set; } = null;
        public int RoleID{ get; set; }
        public string FavoriteBand{ get; set; }
        public int Age { get; set; }
    }
}
