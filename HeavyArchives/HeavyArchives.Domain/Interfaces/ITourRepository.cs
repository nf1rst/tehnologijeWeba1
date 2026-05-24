using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface ITourRepository:IRepository<Tour>
    {
        Task<Tour?> GetTourWithDetailsByNameAsync(string tourName);
        Task<ICollection<Tour>> GetAllToursWithDetailsAsync();
        Task<ICollection<Tour>> GetAllToursWithDetailsByManagerAsync(int managerId); // jel bolj fname i lname umjesto po id
        Task<Tour?> GetTourWithDetailsByIdAsync(int id);
    }
}
