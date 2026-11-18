using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface ICityRepository:IRepository<City>
    {
        Task<City?> GetCityWithDetailsByNameAsync(string name);
        Task<City?> GetCityWithDetailsByIdAsync(int id);
        Task<ICollection<City>> GetCitiesWithDetailsByCountryAsync(string countryName);
        Task<ICollection<City>> GetAllCitiesWithDetails();

    }
}
