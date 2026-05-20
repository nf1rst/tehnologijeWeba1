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

        public ICollection<Concert> GetConcertsWithDetails()
        {
            return context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .ToList();
        }

        public ICollection<Concert> GetConcertsWithDetailsByCity(string cityName)
        {
            return context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .Where(c => c.City.Name == cityName)
                .ToList();
        }

        public ICollection<Concert> GetConcertsWithDetailsByCountry(string countryName)
        {
            return context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .Where(c => c.City.Country.Name == countryName)
                .ToList();
        }

        public ICollection<Concert> GetConcertsWithDetailsByTour(string tourName)
        {
            return context.Concerts
                .Include(c => c.Tour)
                .Include(c => c.City)
                .Where(c => c.Tour.Name == tourName)
                .ToList();
        }

        public Concert? GetConcertWithDetailsById(int id)
        {
            return context.Concerts
                 .Include(c => c.Tour)
                 .Include(c => c.City)
                 .FirstOrDefault(c => c.Id == id);
        }
    }
}
