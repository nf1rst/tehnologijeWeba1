using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Label
{
    public class ManipulateLabelDTO
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }
        public DateOnly FoundingDate { get; set; }
    }
}
