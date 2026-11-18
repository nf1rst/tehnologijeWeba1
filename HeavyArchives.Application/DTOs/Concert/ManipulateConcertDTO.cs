namespace HeavyArchives.Application.DTOs.Concert
{
    public class ManipulateConcertDTO
    {
        public double Price { get; set; }
        public string CityName { get; set; } 
        public DateTime StartingTime { get; set; }
        public string Venue { get; set; } 
        public int Capacity { get; set; }
    }
}
