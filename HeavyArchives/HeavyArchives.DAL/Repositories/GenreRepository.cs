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

        public Genre? GetGenreWithDetails(int id)
        {
            return context.Genres
                .Include(g => g.GenreBand)
                    .ThenInclude(gb => gb.Band)
                .FirstOrDefault(g => g.Id == id);
        }

        public Genre? GetGenre(int id)
        {
            return context.Genres
                .FirstOrDefault(g => g.Id == id);
        }

        public ICollection<Genre> GetAllGenres()
        {
            return context.Genres
                .ToList();
        }

        public ICollection<Genre> GetGenresByBand(string bandName)
        {
            return context.Genres
                .Where(g=>g.GenreBand.Any(gb=>gb.Band.Name==bandName))
                .ToList();
        }

        public Genre? GetGenreByName(string name)
        {
            return context.Genres
                .FirstOrDefault(g => g.Name == name);
        }
    }
}
