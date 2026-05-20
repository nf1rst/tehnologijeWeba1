using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Tour
{
    public class TourDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HeadlinerBand { get; set; }
        public ICollection<BandMinimumInfoDTO> SupportBands { get; set; }
        public DateOnly StartingDate { get; set; }
        public DateOnly FinishingDate { get; set; }
        public ICollection<ConcertDTO> Concerts { get; set; }
        public string TourManger { get; set; }
    }
}
