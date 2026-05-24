using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeavyArchives.DAL.Repositories
{
    public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
    {
        private readonly HeavyArchivesContext context;
        public ConcertRepository(HeavyArchivesContext context):base(context) 
        {
            this.context = context;
        }
        public async Task<ICollection<Concert>> GetConcertsWithDetailsAsync()
        {
            return await context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .ToListAsync();
        }
        public async Task<ICollection<Concert>> GetConcertsWithDetailsByCityAsync(string cityName)
        {
            return await context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .Where(c => c.City.Name == cityName)
                .ToListAsync();
        }



        public async Task<ICollection<Concert>> GetConcertsWithDetailsByCountryAsync(string countryName)
        {
            return await context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .Where(c => c.City.Country.Name == countryName)
                .ToListAsync();
        }

        public async  Task<ICollection<Concert>> GetConcertsWithDetailsByTourAsync(string tourName)
        {
            return await context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .Where(c => c.Tour.Name == tourName)
                .ToListAsync();
        }


        public async Task<Concert?> GetConcertWithDetailsByIdAsync(int id)
        {
            return await  context.Concerts
                 .Include(c => c.Tour)
                 .Include(c => c.City)
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
