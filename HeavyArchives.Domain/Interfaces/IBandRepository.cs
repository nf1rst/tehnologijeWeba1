using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IBandRepository:IRepository<Band>
    {
        Task<Band?> GetBandWithDetailsAsync(int id); 
        Task<ICollection<Band>> GetBandByCountryAsync(string countryName);
        Task<ICollection<Band>> GetBandsByGenreAsync(string genreName);
        Task<ICollection<Band>> GetActiveBandsAsync();
        Task<ICollection<Band>> GetBandsByLabelAsync(int labelId);
        Task<ICollection<Band>> GetBandsWithDetailsAsync();
        Task<Band?> GetBandWithDetailsByNameAsync(string name);
        Task<ICollection<Band>> GetAllBandsAsync();
    }
}
