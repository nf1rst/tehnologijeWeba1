using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        private readonly HeavyArchivesContext context;
        public CityRepository(HeavyArchivesContext context):base(context)
        {
            this.context = context;
        }
        public ICollection<City> GetCitiesWithDetailsByCountry(string countryName)
        {
            return context.Cities
                .Include(c => c.Country)
                .Include(c => c.Concerts)
                .Where(c => c.Country.Name == countryName)
                .ToList();
        }

        public City? GetCityWithDetailsById(int id)
        {
            return context.Cities
                .Include(c => c.Country)
                .Include(c => c.Concerts)
                .FirstOrDefault(c => c.Country.Id == id);
        }

        public City? GetCityWithDetailsByName(string name)
        {
            return context.Cities
                .Include(c => c.Country)
                .Include(c => c.Concerts)
                .FirstOrDefault(c => c.Name == name);
        }
    }
}
