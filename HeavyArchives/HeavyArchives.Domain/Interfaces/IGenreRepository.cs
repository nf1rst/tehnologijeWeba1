using HeavyArchives.Domain.Entities;
namespace HeavyArchives.Domain.Interfaces
{
    public interface IGenreRepository:IRepository<Genre>
    {
        ICollection<Genre> GetGenresByBand(string bandName);
        Genre? GetGenre(int id);
        Genre? GetGenreByName(string name);
        Genre? GetGenreWithDetails(int id);
        ICollection<Genre> GetAllGenres();
    }
}
