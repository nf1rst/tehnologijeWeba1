namespace HeavyArchives.Domain.Entities.M2MTables
{
    public class BandTour
    {
        public int Id{ get; set; }
        public int TourID{ get; set; }
        public int BandId{ get; set; }
        public Band Band{ get; set; }
        public Tour Tour{ get; set; }
    }
}
