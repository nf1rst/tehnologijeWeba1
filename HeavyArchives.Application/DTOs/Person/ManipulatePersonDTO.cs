using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Person
{
    public class ManipulatePersonDTO
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly? DateOfDeath { get; set; }
        public string? CountryOfBirth{ get; set; }
        public char Sex { get; set; }
        public string? ArtistName { get; set; }
    }
}
