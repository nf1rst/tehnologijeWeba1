using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Person
    {
        public int Id{ get; set; }
        public string Fname { get; set; } = String.Empty;
        public string Lname { get; set; } = String.Empty;
        public DateOnly DateOfBirth{ get; set; }
        public DateOnly? DateOfDeath{ get; set; }
        public Country? PlaceOfBirth { get; set; } = null;
        public char Sex{ get; set; }
        public string? ArtistName { get; set; } = String.Empty;
        public ICollection<BandPerson> BandPerson{ get; set; }=new List<BandPerson>();
        public ICollection<Tour> ManagedTours{ get; set; } = new List<Tour>(); // ako je tour manager
    }
}
