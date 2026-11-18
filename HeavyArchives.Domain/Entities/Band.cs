using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Band
    {
        public int Id{ get; set; }
        public string Name { get; set; } = String.Empty;
        public string Status{ get; set; } = String.Empty;
        public int YearOfFormation{ get; set; } = 0;
        public ICollection<GenreBand> GenreBand { get; set; } = new List<GenreBand>();
        public ICollection<BandPerson> BandPerson{ get; set; }=new List<BandPerson>();
        public ICollection<BandTheme> BandTheme{ get; set; }= new List<BandTheme>();
        public ICollection<BandLabel> BandLabel { get; set; } = new List<BandLabel>();
        public ICollection<Album> Albums { get; set; } = new List<Album>();
        public Country Country{ get; set; } = null;
        public int CountryId { get; set; }
        public Contact Contact { get; set; } = null;
        public ICollection<BandTour> TourBand { get; set; } = new List<BandTour>();

    }
}
