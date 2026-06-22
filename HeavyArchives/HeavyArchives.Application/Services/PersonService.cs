using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public PersonService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        //public async Task<PersonWithBandsDTO> AddBandAsync(int bandId, int personId) //ovdje ne treba provjera inputa jer int ne moze bit null
        //{
        //    //Band? band =await uow.Bands.GetBandWithDetailsAsync(bandId);
        //    //if (band is null) throw new EntityNotExistant("Band wasn't found");

        //    Person? person =await uow.Persons.GetPersonWithDetailsAsync(personId);
        //    if (person is null) throw new EntityNotExistant("Person wasn't found");

        //    BandPerson? entry = person.BandPerson.FirstOrDefault(bp => bp.BandId == bandId);
        //    if (entry is null) throw new RelationExistsException("Band is already associated with that person"); // ne postoji veza persona i tog benda

        //    await uow.BandPersons.AddBandPersonAsync(entry);

        //    await uow.SaveChangesAsync();
        //    return mapper.Map<PersonWithBandsDTO>(person);
        //}

        public async Task<PersonDTO> CreatePersonAsync(ManipulatePersonDTO personDTO)
        {
            if (personDTO.DateOfDeath!=null && personDTO.DateOfDeath.Value < personDTO.DateOfBirth) throw new InvalidLogicException("Person's date of death cannot be before persons date of birth");
            //.value jer je dateofDeath? i nece proc unit test 
            
            Country? country = await uow.Countries.GetCountryWithDetailsByNameAsync(personDTO.CountryOfBirth); // je lima veze ako ovdje dode null?
             //samo zato da ga dole mogu ubacit rucno
            if (country is null && personDTO.CountryOfBirth!=null )// u country dodaj novi country
            {
                country = new Country() { Name=personDTO.CountryOfBirth };
                await uow.Countries.InsertAsync(country);
            }

            Person person = mapper.Map<Person>(personDTO);

            person.PlaceOfBirth = country;

            await uow.Persons.InsertAsync(person);

            await uow.SaveChangesAsync();
            return mapper.Map<PersonDTO>(person);
        }

        public async Task DeletePersonAsync(int id)
        {
            Person? person =await uow.Persons.GetByIdAsync(id);
            if (person is null)throw new EntityNotExistant("Person wasn't found"); // ne postoji osoba 

            await uow.Persons.DeleteAsync(person);
            await uow.SaveChangesAsync();
        }
        public async Task<ICollection<PersonDTO>> GetAllPersonsAsync()
        {
            var people =await uow.Persons.GetAllPeopleASync();
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public async Task<ICollection<PersonDTO>> GetDeceasedPeopleAsync()
        {
            var people =await uow.Persons.GetDeceasedPeopleAsync();
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public async Task<ICollection<PersonDTO>> GetLivingPeopleAsync()
        {
            var people =await uow.Persons.GetLivingPeopleAsync();
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public async Task<ICollection<PersonDTO>> GetPeopleByBandAsync(string bandName)
        {
            var people =await uow.Persons.GetPeopleByBandAsync(bandName);
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public async Task<ICollection<PersonDTO>> GetPeopleByCountryAsync(string countryName)
        {
            var people =await uow.Persons.GetPeopleWithDetailByCountryAsync(countryName);
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public async Task<PersonDTO?> GetPersonByIdAsync(int id)
        {
            Person? p =await uow.Persons.GetPersonWithDetailsAsync(id);
            if (p is null) throw new EntityNotExistant("Person wasn't found");
            return mapper.Map<PersonDTO>(p);
        }

        public async Task<PersonDTO> UpdatePersonAsync(int id, ManipulatePersonDTO updatedPerson)
        {
            var person =await uow.Persons.GetPersonWithDetailsAsync(id);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            mapper.Map(updatedPerson, person);
            // ovo ne updatea drzavu, to rucno

            Country? country =await uow.Countries.GetCountryWithDetailsByNameAsync(updatedPerson.CountryOfBirth);

            if(country is null && updatedPerson.CountryOfBirth != null)
            {
                country=new Country() { Name = updatedPerson.CountryOfBirth};
                await uow.Countries.InsertAsync(country);
            }

            person.PlaceOfBirth = country;
            await uow.Persons.UpdateAsync(person);

            await uow.SaveChangesAsync();
            return mapper.Map<PersonDTO>(person);
        }
    }
}
