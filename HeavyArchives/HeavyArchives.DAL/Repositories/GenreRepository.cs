using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        private readonly HeavyArchivesContext context;
        public GenreRepository(HeavyArchivesContext context):base(context){
            this.context = context;
        }


        public async Task<ICollection<Genre>> GetGenresByBandAsync(string bandName)
        {
            return await context.Genres
                .Where(g => g.GenreBand.Any(gb => gb.Band.Name == bandName))
                .ToListAsync();
        }

        public async Task<Genre?> GetGenreAsync(int id)
        {
            return await context.Genres
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre?> GetGenreByNameAsync(string name)
        {
            return await context.Genres
                .FirstOrDefaultAsync(g => g.Name == name);
        }

        public async Task<Genre?> GetGenreWithDetailsAsync(int id)
        {
            return await context.Genres
                .Include(g => g.GenreBand)
                    .ThenInclude(gb => gb.Band)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<ICollection<Genre>> GetAllGenresAsync()
        {
            return await context.Genres
                .ToListAsync();
        }
    }
}
