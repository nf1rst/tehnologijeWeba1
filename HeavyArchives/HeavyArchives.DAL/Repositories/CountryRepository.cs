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

        public Country? GetCountryWithDetailsByName(string name)
        {
            return context.Countries
                .Include(c => c.Persons)
                .Include(c => c.Bands)
                .FirstOrDefault(c => c.Name.Equals(name));
        }


        public Country? GetCountryWithDetailsById(int id)
        {
            return context.Countries
                .Include(c => c.Bands)
                .Include(c => c.Persons)
                .FirstOrDefault(c => c.Id==id);
        }

        public ICollection<Country> GetAllCountries()
        {
            return context.Countries
                .ToList();
        }

        public Country? GetCountryByName(string name)
        {
            return context.Countries
                .FirstOrDefault(c => c.Name == name);
        }

        public Country? GetCountryById(int id)
        {
            return context.Countries
                .FirstOrDefault(c => c.Id== id);
        }
    }
}
