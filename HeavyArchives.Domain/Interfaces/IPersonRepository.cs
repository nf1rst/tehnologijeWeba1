using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IPersonRepository:IRepository<Person>
    {
        Task<ICollection<Person>> GetPeopleByBandAsync(string bandName);
        Task<ICollection<Person>> GetAllPeopleASync();
        Task<ICollection<Person>> GetDeceasedPeopleAsync();
        Task<ICollection<Person>> GetLivingPeopleAsync();
        Task<ICollection<Person>> GetPeopleWithDetailByCountryAsync(string countryName);
        Task<Person?> GetPersonWithDetailsAsync(int id);
        Task<Person?> GetPersonByFirstAndLastNameAsync(string fname, string lname); 
    }
}
