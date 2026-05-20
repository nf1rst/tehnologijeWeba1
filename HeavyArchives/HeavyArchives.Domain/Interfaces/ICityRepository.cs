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
        City? GetCityWithDetailsByName(string name);
        City? GetCityWithDetailsById(int id);
        ICollection<City> GetCitiesWithDetailsByCountry(string countryName);

    }
}
