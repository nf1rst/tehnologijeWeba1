using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Tour
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public DateOnly StartingDate{ get; set; }
        public DateOnly FinishingDate{ get; set; }
        public ICollection<Concert> Concerts{ get; set; }
        public Person TourManager{ get; set; }
        public int TourManagerId{ get; set; }
        public double Revenue { get; set; } = 0;
        public ICollection<BandTour> TourBand{ get; set; } = new List<BandTour>(); //ovdje je boolean property koji kaze hoce li bit support ili headliner band za DTO
    }

}
