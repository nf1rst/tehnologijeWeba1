using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces;

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

        public PersonWithBandsDTO AddBand(int bandId, int personId) //ovdje ne treba provjera inputa jer int ne moze bit null
        {
            Band? band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            Person? person = uow.Persons.GetPersonWithDetails(personId);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            BandPerson? entry = person.BandPerson.FirstOrDefault(bp => bp.BandId == bandId);
            if (entry is null) throw new RelationExistsException("Band is already associated with that person"); // ne postoji veza persona i tog benda

            person.BandPerson.Add(entry);

            uow.SaveChanges();
            return mapper.Map<PersonWithBandsDTO>(person);
        }

        public PersonDTO CreatePerson(ManipulatePersonDTO personDTO)
        {
            Country? country = uow.Countries.GetCountryWithDetailsByName(personDTO.CountryOfBirth); // je lima veze ako ovdje dode null?
             //samo zato da ga dole mogu ubacit rucno
            if (country is null && personDTO.CountryOfBirth!=null )// u country dodaj novi country
            {
                country = new Country() { Name=personDTO.CountryOfBirth };
                uow.Countries.Insert(country);
            }

            Person person = mapper.Map<Person>(personDTO);

            person.PlaceOfBirth = country;

            uow.Persons.Insert(person);

            uow.SaveChanges();
            return mapper.Map<PersonDTO>(person);
        }

        public void DeletePerson(int id)
        {
            Person? person = uow.Persons.GetById(id);
            if (person is null)throw new EntityNotExistant("Person wasn't found"); // ne postoji osoba 

            uow.Persons.Delete(person);
            uow.SaveChanges();
        }
        public ICollection<PersonDTO> GetAllPersons()
        {
            var people = uow.Persons.GetAllPeople();
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public ICollection<PersonDTO> GetDeceasedPeople()
        {
            var people = uow.Persons.GetDeceasedPeople();
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public ICollection<PersonDTO> GetLivingPeople()
        {
            var people = uow.Persons.GetLivingPeople();
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public ICollection<PersonDTO> GetPeopleByBand(string bandName)
        {
            var people = uow.Persons.GetPeopleByBand(bandName);
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public ICollection<PersonDTO> GetPeopleByCountry(string countryName)
        {
            var people = uow.Persons.GetPeopleWithDetailByCountry(countryName);
            return mapper.Map<ICollection<PersonDTO>>(people);
        }

        public PersonDTO? GetPersonById(int id)
        {
            Person? p = uow.Persons.GetPersonWithDetails(id);
            if (p is null) throw new EntityNotExistant("Person wasn't found");
            return mapper.Map<PersonDTO>(p);
        }

        public PersonWithBandsDTO RemoveBand(int bandId, int personId)
        {
            Band? band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            Person? person = uow.Persons.GetPersonWithDetails(personId);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            BandPerson? entry = person.BandPerson.FirstOrDefault(bp => bp.BandId == bandId);
            if (entry is null)throw new NoRelationException("Band is not associated with that person"); // ne postoji veza persona i tog benda

            person.BandPerson.Remove(entry);

            uow.SaveChanges();
            return mapper.Map<PersonWithBandsDTO>(person);
        }

        public PersonDTO UpdatePerson(int id, ManipulatePersonDTO updatedPerson)
        {
            var person = uow.Persons.GetPersonWithDetails(id);
            if (person is null) throw new EntityNotExistant("Person wasn't found");

            mapper.Map(updatedPerson, person);
            // ovo ne updatea drzavu, to rucno

            Country? country = uow.Countries.GetCountryWithDetailsByName(updatedPerson.CountryOfBirth);

            if(country is null && updatedPerson.CountryOfBirth != null)
            {
                country=new Country() { Name = updatedPerson.CountryOfBirth};
                uow.Countries.Insert(country);
            }

            person.PlaceOfBirth = country;
            uow.Persons.Update(person);

            uow.SaveChanges();
            return mapper.Map<PersonDTO>(person);
        }
    }
}
