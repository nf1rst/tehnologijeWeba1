using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface ICountryRepository:IRepository<Country>
    {
        Task<Country?> GetCountryWithDetailsByNameAsync(string name);
        Task<Country?> GetCountryWithDetailsByIdAsync(int id);
        Task<ICollection<Country>> GetAllCountriesAsync();
        Task<Country?> GetCountryByNameAsync(string name);
        Task<Country?> GetCountryByIdAsync(int id);
    }
}
