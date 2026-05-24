using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface IBandService
    {
        Task<BandWithMembersDTO> GetBandByIdAsync(int id);
        Task<BandWithMembersDTO >GetBandByNameAsync(string name);
        Task<ICollection<BandMinimumInfoDTO>> GetAllBandsAsync();
        Task<ICollection<BandMinimumInfoDTO> >GetBandsByCountryAsync(string countryName);//ova 2 su bez members
        Task<ICollection<BandMinimumInfoDTO>> GetBandsByGenreAsync(string genreName);
        Task<ICollection<BandMinimumInfoDTO>> GetActiveBandsAsync();
        Task<BandDTO >CreateBandAsync(ManipulateBandDTO band);
        Task<ICollection<BandMinimumInfoDTO>> GetBandsDetailsWithMembersAsync();
        Task<BandMinimumInfoDTO> UpdateBandAsync(int id, ManipulateBandDTO updatedBand);
        Task DeleteBandAsync(int id);
        Task<BandWithMembersDTO > AddMemberAsync(int bandId, int personId, string status, string role);
        Task<BandWithMembersDTO >RemoveMemberAsync(int bandId, int personId);
        Task<BandWithMembersDTO >AddGenreAsync(int bandId, int genreId);
        Task<BandWithMembersDTO > RemoveGenreAsync(int bandId, int genreId);
    }
}
