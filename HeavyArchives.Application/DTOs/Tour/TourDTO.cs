using HeavyArchives.Application.DTOs.Concert;

namespace HeavyArchives.Application.DTOs.Tour
{
    public class TourDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string HeadlinerBand { get; set; } = String.Empty;
        public ICollection<String> SupportBands { get; set; } = new List<String>();
        public DateOnly StartingDate { get; set; }
        public DateOnly FinishingDate { get; set; }
        public ICollection<ConcertDTO> Concerts { get; set; } = new List<ConcertDTO>();
        public string TourManger { get; set; } = String.Empty;
        public double Revenue{get; set; }
    }
}
