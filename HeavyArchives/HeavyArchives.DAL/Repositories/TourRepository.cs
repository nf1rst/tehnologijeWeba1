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
        public ICollection<Tour> GetAllToursWithDetails()
        {
            return context.Tours
                .Include(t => t.Concerts)
                .Include(t => t.SupportBands)
                .Include(t => t.HeadlinerBand)
                .Include(t => t.TourManager)
                .Include(t => t.TourBand)
                    .ThenInclude(tb => tb.Band)
                .ToList();
        }

        public ICollection<Tour> GetAllToursWithDetailsByManager(int managerId)
        {
            return context.Tours
                .Include(t => t.Concerts)
                .Include(t => t.SupportBands)
                .Include(t => t.HeadlinerBand)
                .Include(t => t.TourManager)
                .Include(t => t.TourBand)
                    .ThenInclude(tb => tb.Band)
                .Where(t=>t.TourManager.Id==managerId)
                .ToList();
        }

        public Tour? GetTourWithDetailsById(int id)
        {
            return context.Tours
                .Include(t => t.Concerts)
                .Include(t => t.SupportBands)
                .Include(t => t.HeadlinerBand)
                .Include(t => t.TourManager)
                .Include(t => t.TourBand)
                    .ThenInclude(tb => tb.Band)
                .FirstOrDefault(t => t.Id == id);
        }

        public Tour? GetTourWithDetailsByName(string tourName)
        {
            return context.Tours
             .Include(t => t.Concerts)
             .Include(t => t.SupportBands)
             .Include(t => t.HeadlinerBand)
             .Include(t => t.TourManager)
             .Include(t => t.TourBand)
                 .ThenInclude(tb => tb.Band)
             .FirstOrDefault(t => t.Name == tourName);
        }

    }
}
