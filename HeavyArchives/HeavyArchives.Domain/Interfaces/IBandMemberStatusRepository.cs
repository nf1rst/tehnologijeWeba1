using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IBandMemberStatusRepository:IRepository<BandMemberStatus>
    {
        Task<BandMemberStatus?> GetStatusWithDetailsByIDAsync(int id);
        Task<BandMemberStatus?> GetStatusWithDetailsByNameAsync(string name);

        Task<ICollection<BandMemberStatus>> GetAllStatusesWithDetailsAsync();

    }
}
