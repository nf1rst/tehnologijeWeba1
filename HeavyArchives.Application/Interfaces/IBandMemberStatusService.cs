using HeavyArchives.Application.DTOs.BandMemberStatus;

namespace HeavyArchives.Application.Interfaces
{
    public interface IBandMemberStatusService
    {
        Task<ICollection<BandMemberStatusDTO>> GetAllStatusesAsync();
        Task<BandMemberStatusDTO> CreateBandMemberStatusAsync(ManipulateBandMemberStatusDTO createDTO);
        Task DeleteBandMemberStatusAsync(int id);
        Task<BandMemberStatusDTO> UpdateBandMemberStatusAsync(int id, ManipulateBandMemberStatusDTO updateDTO);
    }
}
