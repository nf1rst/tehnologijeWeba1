using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Person
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly? DateOfDeath { get; set; }
        public string PlaceOfBirth { get; set; }
        public char Sex { get; set; }
        public string ArtistName { get; set; }
        
        //jel treba tu dodat da se pokaze i bandDTO
    }
}
