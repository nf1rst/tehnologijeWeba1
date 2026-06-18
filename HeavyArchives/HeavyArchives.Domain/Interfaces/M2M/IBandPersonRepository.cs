using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;

namespace HeavyArchives.Domain.Interfaces.M2M
{
    public  interface IBandPersonRepository
    {
        Task AddBandPersonAsync(BandPerson bandPerson);
        Task RemoveBandPersonAsync(BandPerson bandPerson);
        Task<BandPerson?> GetBandPersonByParamAsync(int personId, int bandId);
        Task<ICollection<BandPerson>> GetBandPersonByPersonId(int personId);

    }
}
