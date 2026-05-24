using HeavyArchives.Application.DTOs.Concert;

namespace HeavyArchives.Application.Interfaces
{
    public interface IConcertService
    {
        Task<ICollection<ConcertDTO>> GetConcertsForTourAsync(string tourName);
        Task<ICollection<ConcertDTO>> GetConcertsForCityAsync(string cityName);
        Task<ICollection<ConcertDTO>> GetConcertsForCountryAsync(string countryName);
        Task<ICollection<ConcertDTO>> GetAllConcertsAsync();
        Task<ConcertDTO> CreateConcertAsync(ManipulateConcertDTO createDTO);
        Task<ConcertDTO  >UpdateConcertAsync(int id, ManipulateConcertDTO updateDTO);
        Task DeleteConcertAsync(int id);

    }
}
