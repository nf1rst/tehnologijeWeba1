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
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        protected readonly HeavyArchivesContext context;
        public CountryRepository(HeavyArchivesContext context):base(context){
        this.context = context;
        }
        public async Task<Country?> GetCountryWithDetailsByNameAsync(string name)
        {
            return await context.Countries
                .Include(c => c.Persons)
                .Include(c => c.Bands)
                .FirstOrDefaultAsync(c => c.Name.Equals(name));
        }

        public async Task<Country?> GetCountryWithDetailsByIdAsync(int id)
        {
            return await  context.Countries
                .Include(c => c.Bands)
                .Include(c => c.Persons)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Country>> GetAllCountriesAsync()
        {
            return await context.Countries
                .ToListAsync();
        }

        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await context.Countries
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await context.Countries
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
