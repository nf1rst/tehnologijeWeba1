using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.DTOs.Theme;
using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Band
{
    public class BandDTO
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Status{ get; set; }
        public int YearOfFormation{ get; set; }
        public string CoutnryName{ get; set; }
        public ICollection<GenreDTO> Genres{ get; set; }=new List<GenreDTO>();
        public ICollection<ThemeDTO> Themes{ get; set; }=new List<ThemeDTO>();
        
    }
}
