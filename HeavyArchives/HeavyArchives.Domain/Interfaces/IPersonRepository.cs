using HeavyArchives.Domain.Entities;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IPersonRepository:IRepository<Person>
    {
        ICollection<Person> GetPeopleByBand(string bandName);
        ICollection<Person> GetAllPeople();
        ICollection<Person> GetDeceasedPeople();
        ICollection<Person> GetLivingPeople();
        ICollection<Person> GetPeopleWithDetailByCountry(string countryName);
        Person? GetPersonWithDetails(int id);
        Person? GetPersonByFirstAndLastName(string fname, string lname); // ovo se potencijalno treba micat
    }
}
