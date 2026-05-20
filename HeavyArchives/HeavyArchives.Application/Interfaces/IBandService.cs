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
        BandWithMembersDTO GetBandById(int id);
        BandWithMembersDTO GetBandByName(string name);
        ICollection<BandMinimumInfoDTO> GetAllBands();
        ICollection<BandMinimumInfoDTO> GetBandsByCountry(string countryName);//ova 2 su bez members
        ICollection<BandMinimumInfoDTO> GetBandsByGenre(string genreName);
        ICollection<BandMinimumInfoDTO> GetActiveBands();
        BandDTO CreateBand(ManipulateBandDTO band);
        ICollection<BandMinimumInfoDTO> GetBandsDetailsWithMembers();
        BandMinimumInfoDTO UpdateBand(int id, ManipulateBandDTO updatedBand);
        void DeleteBand(int id);
        BandWithMembersDTO AddMember(int bandId, int personId, string status, string role);
        BandWithMembersDTO RemoveMember(int bandId, int personId);
        BandWithMembersDTO AddGenre(int bandId, int genreId);
        BandWithMembersDTO RemoveGenre(int bandId, int genreId);
    }
}
