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

        public ICollection<Band> GetActiveBands()
        {
            return context.Bands
                .Include(b=>b.Country)
                .Where(b => b.Status == "Active")
                .ToList();
        }

        public ICollection<Band> GetBandByCountry(string countryName)
        {
            return context.Bands
                .Include(b => b.Country)
                .Where(b => b.Country.Name == countryName)
                .ToList();
        }

        public ICollection<Band> GetBandsByGenre(string genreName)
        {
            return context.Bands
                .Include(b => b.Country)
                    .Where(b => b.GenreBand.Any(gb => gb.Genre.Name==genreName))
                    .ToList(); 
        }

        public ICollection<Band> GetBandsByLabel(int labelId)
        {
            return context.Bands
                .Include(b => b.BandLabel)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Status)
                .Where(b => b.BandLabel.Any(bl => bl.LabelId == labelId))
                .ToList();
        }
        public ICollection<Band> GetBandsWithDetails()
        {
            var bands = context.Bands
                .Include(b => b.Country)
                .Include(b => b.GenreBand)
                    .ThenInclude(gb=>gb.Genre)
                .Include(b => b.Albums)
                .Include(b => b.BandLabel)
                    .ThenInclude(bl => bl.Label)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Person)
                .Include(b => b.BandTheme)
                    .ThenInclude(b => b.Theme)
                .Include(b => b.Contact)
                .Include(b=>b.BandPerson)
                    .ThenInclude(bp=>bp.Status)
                .ToList();
            return bands;
        }

        public Band? GetBandWithDetails(int id)
        {
            return context.Bands
                .Include(b => b.Country)
                .Include(b => b.GenreBand)
                    .ThenInclude(gb=>gb.Genre)
                .Include(b => b.Albums)
                .Include(b => b.BandLabel)
                    .ThenInclude(bl=>bl.Label)
                .Include(b => b.BandPerson)
                    .ThenInclude(b=>b.Person)
                .Include(b => b.BandTheme)
                    .ThenInclude(b=>b.Theme)
                .Include(b => b.Contact)
                .Include(b => b.BandPerson)
                    .ThenInclude(bp => bp.Status)
                .FirstOrDefault(b => b.Id == id);
        }

        public Band? GetBandWithDetailsByName(string name)
        {
            return context.Bands
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
                .FirstOrDefault(b=>b.Name==name);
        }

        public ICollection<Band> GetAllBands()
        { 
            return context.Bands
                .Include(b=>b.Country)
                .ToList();
        }
    }
}
