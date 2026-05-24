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

        public async Task<Album?> GetAlbumByNameAsync(string name)
        {
            return await context.Albums
                .Include(a => a.Band)
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<ICollection<Album>> GetAlbumsByBandAsync(int bandId)
        {
            return await context.Albums.
                 Include(a => a.Band)
                 .Where(a => a.BandId == bandId)
                 .ToListAsync();
        }


        public async Task<ICollection<Album>> GetAlbumsByBandNameAsync(string bandName)
        {
            return await context.Albums
                .Include(a => a.Band)
                .Where(a => a.Band.Name == bandName)
                .ToListAsync();
        }

        public async Task<ICollection<Album>> GetAlbumsByTypeAsync(string type)
        {
            return await context.Albums
                 .Include(a => a.Band)
                 .Where(a => a.Type.Equals(type))
                 .ToListAsync();
        }

        public async Task<ICollection<Album>> GetAlbumsByYearAsync(int year)
        {
            return await context.Albums
                .Include(a => a.Band)
                .Where(a => a.DateOfPublishing.Year == year)
                .ToListAsync();
        }

        public async Task<ICollection<Album>> GetAlbumsWithDetailsAsync()
        {
            return await context.Albums
                .Include(a => a.Band)
                .ToListAsync();
        }

        public async Task<Album?> GetAlbumWithDetailsByIdAsync(int id)
        {
            return await context.Albums
                .Include(a => a.Band)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
