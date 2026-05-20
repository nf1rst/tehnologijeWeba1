using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IBandMemberStatusRepository:IRepository<BandMemberStatus>
    {
        BandMemberStatus? GetStatusWithDetailsByID(int id);
        BandMemberStatus? GetStatusWithDetailsByName(string name);

        ICollection<BandMemberStatus> GetAllStatusesWithDetails();

    }
}
