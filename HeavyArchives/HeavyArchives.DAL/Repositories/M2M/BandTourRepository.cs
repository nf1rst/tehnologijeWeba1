using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces.M2M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories.M2M
{
    public class BandTourRepository : IBandTourRepository
    {
        private readonly HeavyArchivesContext context;
        public BandTourRepository(HeavyArchivesContext context)
        {
            this.context = context;
        }
        public async Task AddBandTourAsync(BandTour bandTour)
        {
            await context.BandTours.AddAsync(bandTour);
        }
    }
}
