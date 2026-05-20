using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.DTOs.Person;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface IPersonService
    {
        PersonDTO? GetPersonById(int id);
        ICollection<PersonDTO> GetAllPersons();

        PersonDTO CreatePerson(ManipulatePersonDTO person);
        ICollection<PersonDTO> GetPeopleByCountry(string country);
        ICollection<PersonDTO> GetLivingPeople();
        ICollection<PersonDTO> GetDeceasedPeople();
        ICollection<PersonDTO> GetPeopleByBand(string bandName);
        PersonDTO UpdatePerson(int id, ManipulatePersonDTO updatedPerson);
        void DeletePerson(int id);
        PersonWithBandsDTO AddBand(int bandId, int personId);
        PersonWithBandsDTO RemoveBand(int bandId, int personId);


    }
}
