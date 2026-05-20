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
        ICollection<Album> GetAlbumsByBand(int bandId);// !OVO SE MOZDA NE KORISTI NIGDJE
        ICollection<Album> GetAlbumsByBandName(string bandName);
        ICollection<Album> GetAlbumsByYear(int year);
        ICollection<Album> GetAlbumsByType(string type);

        ICollection<Album> GetAlbumsWithDetails();
        Album? GetAlbumWithDetailsById(int id);
        Album? GetAlbumByName(string name);

    }
}
