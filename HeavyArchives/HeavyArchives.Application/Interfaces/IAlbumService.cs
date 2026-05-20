using HeavyArchives.Application.DTOs.Album;
using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface IAlbumService
    {
        ICollection<AlbumDTO> GetAllAlbums();
        AlbumDTO GetAlbumById(int id);
        ICollection<AlbumDTO> GetAlbumsByBand(string bandName);
        ICollection<AlbumDTO> GetAlbumsByYear(int year);
        AlbumDTO UpdateAlbum(int id, UpdateAlbuumDTO updatedBandDTO);
        void DeleteAlbum(int id);
        AlbumDTO CreateAlbum(CreateAlbumDTO bandDTO);
        AlbumDTO GetAlbumByName(string name);
        ICollection<AlbumDTO> GetAlbumsByType(string type);
    }
}
