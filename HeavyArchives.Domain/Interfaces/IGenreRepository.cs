using HeavyArchives.Domain.Entities;
namespace HeavyArchives.Domain.Interfaces
{
    public interface IGenreRepository:IRepository<Genre>
    {
        Task<ICollection<Genre>> GetGenresByBandAsync(string bandName);
        Task<Genre?> GetGenreAsync(int id);
        Task<Genre? >GetGenreByNameAsync(string name);
        Task<Genre?> GetGenreWithDetailsAsync(int id);
        Task<ICollection<Genre>> GetAllGenresAsync();
    }
}
