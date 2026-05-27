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
    public class TourRepository : BaseRepository<Tour>, ITourRepository
    {
        private readonly HeavyArchivesContext context;
        public TourRepository(HeavyArchivesContext context):base(context)
        {
            this.context= context;
        }

        public async Task<ICollection<Tour>> GetAllToursWithDetailsAsync()
        {
            return await context.Tours
                .Include(t => t.Concerts)
                    .ThenInclude(c=>c.City)
                .Include(t => t.TourBand)
                .ThenInclude(tb => tb.Band)
                .Include(t => t.TourManager)
                .Include(t => t.TourBand)
                    .ThenInclude(tb => tb.Band)
                .ToListAsync();
        }



        public async Task<ICollection<Tour>> GetAllToursWithDetailsByManagerAsync(int managerId)
        {
            return await  context.Tours
                .Include(t => t.Concerts)
             .Include(t => t.TourBand)
                .ThenInclude(tb => tb.Band)
                .Include(t => t.TourManager)
                .Include(t => t.TourBand)
                    .ThenInclude(tb => tb.Band)
                .Where(t => t.TourManager.Id == managerId)
                .ToListAsync();
        }

        public async Task<Tour?> GetTourWithDetailsByIdAsync(int id)
        {
            return await context.Tours
                .Include(t => t.Concerts)
             .Include(t => t.TourBand)
                .ThenInclude(tb => tb.Band)
                .Include(t => t.TourManager)
                .Include(t => t.TourBand)
                    .ThenInclude(tb => tb.Band)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tour?> GetTourWithDetailsByNameAsync(string tourName)
        {
            return await context.Tours
             .Include(t => t.Concerts)
             .Include(t=>t.TourBand)
                .ThenInclude(tb=>tb.Band)
             .Include(t => t.TourManager)
             .Include(t => t.TourBand)
                 .ThenInclude(tb => tb.Band)
             .FirstOrDefaultAsync(t => t.Name == tourName);
        }
    }
}
