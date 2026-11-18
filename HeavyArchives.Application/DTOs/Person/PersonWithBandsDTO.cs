using HeavyArchives.Application.DTOs.Band;

namespace HeavyArchives.Application.DTOs.Person
{
    public class PersonWithBandsDTO
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string ArtistName { get; set; }
        public ICollection<BandMinimumInfoDTO> Bands{ get; set; }
    }
}
