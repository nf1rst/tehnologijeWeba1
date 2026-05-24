using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public async Task<ICollection<City>> GetCitiesWithDetailsByCountryAsync(string countryName)
        {
            return await context.Cities
                .Include(c => c.Country)
                .Include(c => c.Concerts)
                .Where(c => c.Country.Name == countryName)
                .ToListAsync();
        }



        public async Task<City?> GetCityWithDetailsByIdAsync(int id)
        {
            return await context.Cities
                .Include(c => c.Country)
                .Include(c => c.Concerts)
                .FirstOrDefaultAsync(c => c.Country.Id == id);
        }

        public async Task<City?> GetCityWithDetailsByNameAsync(string name)
        {
            return await context.Cities
                .Include(c => c.Country)
                .Include(c => c.Concerts)
                .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
