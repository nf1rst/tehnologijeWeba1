using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeavyArchives.DAL.Repositories
{
    public class BandRepository : BaseRepository<Band>, IBandRepository 
    {
        protected readonly HeavyArchivesContext context;
        public BandRepository(HeavyArchivesContext context) : base(context) {
            this.context = context; //bez ovoga ne radi iako je to u base repository
        }

        public async Task<ICollection<Band>> GetActiveBandsAsync()
        {
            return await context.Bands
                .Include(b=>b.Country)
                .Where(b => b.Status == "Active")
                .ToListAsync();
        }

        public async Task<Band?> GetBandWithDetailsAsync(int id)
        {
            return await context.Bands
                .Include(b => b.Country)
                .Include(b => b.GenreBand)
                    .ThenInclude(gb => gb.Genre)
                .Include(b => b.Albums)
                .Include(b => b.BandLabel)
                    .ThenInclude(bl => bl.Label)
                .Include(b => b.BandPerson)
                    .ThenInclude(b => b.Person)
                .Include(b => b.BandTheme)
                    .ThenInclude(b => b.Theme)
                .Include(b => b.Contact)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Status)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<ICollection<Band>> GetBandByCountryAsync(string countryName)
        {
            return await  context.Bands
                 .Include(b => b.Country)
                 .Where(b => b.Country.Name == countryName)
                 .ToListAsync();
        }

        public async Task<ICollection<Band>> GetBandsByGenreAsync(string genreName)
        {
            return await context.Bands
                 .Include(b => b.Country)
                     .Where(b => b.GenreBand.Any(gb => gb.Genre.Name == genreName))
                     .ToListAsync();
        }

        public async Task<ICollection<Band>> GetBandsByLabelAsync(int labelId)
        {
            return await context.Bands
                .Include(b => b.BandLabel)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Status)
                .Where(b => b.BandLabel.Any(bl => bl.LabelId == labelId))
                .ToListAsync();
        }

        public async Task<ICollection<Band>> GetBandsWithDetailsAsync()
        {
            return await context.Bands
                .Include(b => b.Country)
                .Include(b => b.GenreBand)
                    .ThenInclude(gb => gb.Genre)
                .Include(b => b.Albums)
                .Include(b => b.BandLabel)
                    .ThenInclude(bl => bl.Label)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Person)
                .Include(b => b.BandTheme)
                    .ThenInclude(b => b.Theme)
                .Include(b => b.Contact)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Status)
                .ToListAsync();
        }

        public async Task<Band?> GetBandWithDetailsByNameAsync(string name)
        {
            return await context.Bands
                .Include(b => b.Country)
                .Include(b => b.GenreBand)
                    .ThenInclude(gb => gb.Genre)
                .Include(b => b.Albums)
                .Include(b => b.BandLabel)
                    .ThenInclude(bl => bl.Label)
                .Include(b => b.BandPerson)
                    .ThenInclude(b => b.Person)
                .Include(b => b.BandTheme)
                    .ThenInclude(b => b.Theme)
                .Include(b => b.Contact)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Status)
                .FirstOrDefaultAsync(b => b.Name == name);
        }

        public async Task<ICollection<Band>> GetAllBandsAsync()
        {
            return await context.Bands
                 .Include(b => b.Country)
                 .ToListAsync();
        }
    }
}
