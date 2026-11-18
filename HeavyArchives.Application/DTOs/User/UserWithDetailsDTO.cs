using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.DTOs.User
{
    public class UserWithDetailsDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public string PasswordSalt { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Role { get; set; } = String.Empty;
    }
}
