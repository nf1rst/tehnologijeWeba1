using HeavyArchives.Application.DTOs.Tour;

namespace HeavyArchives.Application.Interfaces
{
    public interface ITourService
    {
        Task<TourDTO?> GetTourByIdAsync(int id);
        Task<TourDTO?> GetTourByNameAsync(string tourName);
        Task<ICollection<TourDTO>> GetAllToursAsync();
        Task<ICollection<TourDTO>> GetAllToursByManagerAsync(string manager);
        Task<TourDTO> AddSupportBandAsync(int tourId, int bandId);
        Task<TourDTO> RemoveSupportBandAsync(int tourId, int bandId);
        Task<TourDTO> AddConcertAsync(int tourId, int concertId);
        Task<TourDTO> RemoveConcertAsync(int tourId, int concertId);
        Task DeleteAsync(int id);
        Task<TourDTO> CreateTourAsync(ManipulateTourDTO createDTO);
        Task<TourDTO> UpdateTourAsync(int id, ManipulateTourDTO updateDTO);

    }
}
