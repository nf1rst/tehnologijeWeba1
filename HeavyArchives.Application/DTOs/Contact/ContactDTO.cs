using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Contact
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string? Tel { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public string Band { get; set; }
    }
}
