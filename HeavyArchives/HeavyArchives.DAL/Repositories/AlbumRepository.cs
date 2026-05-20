using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        protected readonly HeavyArchivesContext context;
        public AlbumRepository(HeavyArchivesContext context) : base(context) { 

            this.context = context;
        }

        public Album? GetAlbumByName(string name)
        {
            return context.Albums
                .Include(a=>a.Band)
                .FirstOrDefault(a => a.Name == name);
        }

        public ICollection<Album> GetAlbumsByBand(int bandId)
        {
           return context.Albums.
                Include(a => a.Band)
                .Where(a => a.BandId == bandId)
                .ToList();

        }

        public ICollection<Album> GetAlbumsByBandName(string bandName)
        {
            return context.Albums
                .Include(a => a.Band)
                .Where(a => a.Band.Name == bandName)
                .ToList();
        }
        

        public ICollection<Album> GetAlbumsByType(string type)
        {
            return context.Albums
                .Include(a => a.Band)
                .Where(a => a.Type.Equals(type))
                .ToList();
        }

        public ICollection<Album> GetAlbumsByYear(int year)
        {
            return context.Albums
                .Include(a => a.Band)
                .Where(a => a.DateOfPublishing.Year == year)
                .ToList();

        }

        public ICollection<Album> GetAlbumsWithDetails()
        {
            return context.Albums
                .Include(a => a.Band)
                .ToList();
        }

        public Album? GetAlbumWithDetailsById(int id)
        {
            return context.Albums
                .Include(a => a.Band)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
