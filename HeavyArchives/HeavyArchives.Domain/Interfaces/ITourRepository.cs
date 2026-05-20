using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface ITourRepository:IRepository<Tour>
    {
        Tour? GetTourWithDetailsByName(string tourName);
        ICollection<Tour> GetAllToursWithDetails();
        ICollection<Tour> GetAllToursWithDetailsByManager(int managerId); // jel bolj fname i lname umjesto po id
        Tour? GetTourWithDetailsById(int id);
    }
}
