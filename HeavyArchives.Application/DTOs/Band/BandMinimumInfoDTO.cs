using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Band
{
    public class BandMinimumInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int YearOfFormation { get; set; }
        public string CoutnryName { get; set; }
    }
}
