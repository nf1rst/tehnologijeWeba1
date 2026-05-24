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
    public class BandMemberStatusRepository:BaseRepository<BandMemberStatus>, IBandMemberStatusRepository
    {
        private readonly HeavyArchivesContext context;
        public BandMemberStatusRepository(HeavyArchivesContext context):base(context)
        {
            this.context = context;
        }

        public  async Task<ICollection<BandMemberStatus>> GetAllStatusesWithDetailsAsync()
        {
            return await context.Statuses
                .Include(s => s.BandPerson)
                    .ThenInclude(bp => bp.Person)
                .ToListAsync();
        }

        public async Task<BandMemberStatus?> GetStatusWithDetailsByIDAsync(int id)
        {
            return await  context.Statuses
                 .Include(s => s.BandPerson)
                     .ThenInclude(bp => bp.Person)
                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<BandMemberStatus?> GetStatusWithDetailsByNameAsync(string name)
        {
            return await  context.Statuses
                 .Include(s => s.BandPerson)
                     .ThenInclude(bp => bp.Person)
                 .FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
