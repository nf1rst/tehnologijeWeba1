using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.DTOs.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Label
{
    public class LabelWithMembersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }
        public DateOnly FoundingDate { get; set; }
        public ICollection<BandMinimumInfoDTO> Bands { get; set; }
    }
}
