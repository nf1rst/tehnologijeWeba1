using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface ICountryRepository:IRepository<Country>
    {
        Country? GetCountryWithDetailsByName(string name);
        Country? GetCountryWithDetailsById(int id);
        ICollection<Country> GetAllCountries();
        Country? GetCountryByName(string name);
        Country? GetCountryById(int id);
    }
}
