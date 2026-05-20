using HeavyArchives.Application.DTOs.Contact;
using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Band
{
    public class BandWithMembersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public int YearOfFormation { get; set; } = 0;
        public string CoutnryName { get; set; } = String.Empty;
        public ICollection<PersonFewDetailsDTO> CurrentMembers{ get; set; }=new List<PersonFewDetailsDTO>();
        public ICollection<PersonFewDetailsDTO> PastMembers{ get; set; }= new List<PersonFewDetailsDTO>();
        public ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public ContactDTO Contact { get; set; } = null;
    }
}
