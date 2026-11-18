using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Band
{
    public class ManipulateBandDTO
    {
        public string Name { get; set; }
        public string Status { get; set; } 
        public int YearOfFormation { get; set; }
        public string CounrtyName{ get; set; } 
    }
}
