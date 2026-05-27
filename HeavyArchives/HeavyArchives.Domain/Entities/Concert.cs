using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Concert
    {
        public int Id{ get; set; }
        public int? TourId{ get; set; }
        public Tour? Tour{ get; set; }
        public double Price{ get; set; }
        public City City{ get; set; }
        public int CityId{ get; set; }
        public DateTime StartingTime{ get; set; }
        public string Venue{ get; set; } // posebni property?
    }
}
