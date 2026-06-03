using HeavyArchives.Domain.Entities.M2MTables;

namespace HeavyArchives.Domain.Interfaces.M2M
{
    public  interface IBandPersonRepository
    {
        Task AddBandPersonAsync(BandPerson bandPerson);
        Task RemoveBandPersonAsync(BandPerson bandPerson);
    }
}
