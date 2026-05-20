namespace HeavyArchives.Application.DTOs.Concert
{
    public class ManipulateConcertDTO
    {

        public string TourName { get; set; } // ako ne postoji baca exception i ne stavlja nista
        public double Price { get; set; }
        public string CityName { get; set; } // ako ne postoji opet isto
        public DateTime StartingTime { get; set; }
        public string Venue { get; set; } // posebni property?
    }
}
