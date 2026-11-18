using HeavyArchives.Application.DTOs.Concert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.DTOs.City
{
    public class CityWithDetailsDTO
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string CoutnryName{ get; set; }
        public ICollection<ConcertDTO> Concerts { get; set; }
    }
}
