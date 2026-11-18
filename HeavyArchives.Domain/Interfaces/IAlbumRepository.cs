using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IAlbumRepository:IRepository<Album>
    {
        Task<ICollection<Album>> GetAlbumsByBandAsync(int bandId);
        Task<ICollection<Album>> GetAlbumsByBandNameAsync(string bandName);
        Task<ICollection<Album>> GetAlbumsByYearAsync(int year);
        Task<ICollection<Album>> GetAlbumsByTypeAsync(string type);

        Task<ICollection<Album>> GetAlbumsWithDetailsAsync();
        Task<Album?> GetAlbumWithDetailsByIdAsync(int id);
        Task<Album?> GetAlbumByNameAsync(string name);

    }
}
