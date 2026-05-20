using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Tour
{
    public class ManipulateTourDTO
    {
        public string Name { get; set; }
        public string HeadlinerBand { get; set; }
        public DateOnly StartingDate { get; set; }
        public DateOnly FinishingDate { get; set; }
        public string TourManager { get; set; }
    }
}
