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

        public ICollection<BandMemberStatus> GetAllStatusesWithDetails()
        {
            return context.Statuses
                .Include(s => s.BandPerson)
                    .ThenInclude(bp => bp.Person)
                .ToList();
        }

        public BandMemberStatus? GetStatusWithDetailsByID(int id)
        {
            return context.Statuses
                .Include(s => s.BandPerson)
                    .ThenInclude(bp => bp.Person)
                .FirstOrDefault(s => s.Id == id);
        }

        public BandMemberStatus? GetStatusWithDetailsByName(string name)
        {
            return context.Statuses
                .Include(s => s.BandPerson)
                    .ThenInclude(bp => bp.Person)
                .FirstOrDefault(s => s.Name==name);
        }


    }
}
