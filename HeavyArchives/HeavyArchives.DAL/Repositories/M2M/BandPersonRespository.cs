using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces.M2M;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories.M2M
{
    public class BandPersonRespository : IBandPersonRepository
    {
        private readonly HeavyArchivesContext context;
        public BandPersonRespository(HeavyArchivesContext context)
        {
            this.context = context;
        }

        public async Task AddBandPersonAsync(BandPerson bandPerson)
        {
            await context.BandPersons.AddAsync(bandPerson);
        }

        public async Task<BandPerson> GetPersonFromBandByPersonIdAsync(int personId, int bandId)
        {
            return await context.BandPersons
                .Where(bp => bp.BandId == bandId)
                .FirstOrDefaultAsync(bp => bp.PersonId == personId);
        }

        public Task RemoveBandPersonAsync(BandPerson bandPerson)
        {
            context.BandPersons.Remove(bandPerson);
            return Task.CompletedTask;
        }
    }
}
