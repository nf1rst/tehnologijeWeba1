using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Contact
    {
        public int Id{ get; set; }
        public string? Tel { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string? Website { get; set; } = String.Empty;
        public Band Band { get; set; } = null;
        public int BandId{ get; set; }
    }
}
