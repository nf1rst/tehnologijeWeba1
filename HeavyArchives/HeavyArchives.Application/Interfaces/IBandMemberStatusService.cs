using HeavyArchives.Application.DTOs.BandMemberStatus;

namespace HeavyArchives.Application.Interfaces
{
    public interface IBandMemberStatusService
    {
        ICollection<BandMemberStatusDTO> GetAllStatuses();
        BandMemberStatusDTO CreateBandMemberStatus(ManipulateBandMemberStatusDTO createDTO);
        void DeleteBandMemberStatus(int id);
        BandMemberStatusDTO UpdateBandMemberStatus(int id, ManipulateBandMemberStatusDTO updateDTO);
    }
}
