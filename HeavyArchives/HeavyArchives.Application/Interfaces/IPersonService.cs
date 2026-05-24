using HeavyArchives.Application.DTOs.Person;

namespace HeavyArchives.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDTO?> GetPersonByIdAsync(int id);
        Task<ICollection<PersonDTO>> GetAllPersonsAsync();

        Task<PersonDTO> CreatePersonAsync(ManipulatePersonDTO person);
        Task<ICollection<PersonDTO>> GetPeopleByCountryAsync(string country);
        Task<ICollection<PersonDTO>> GetLivingPeopleAsync();
        Task<ICollection<PersonDTO>> GetDeceasedPeopleAsync();
        Task<ICollection<PersonDTO>> GetPeopleByBandAsync(string bandName);
        Task<PersonDTO> UpdatePersonAsync(int id, ManipulatePersonDTO updatedPerson);
        Task DeletePersonAsync(int id);
        Task<PersonWithBandsDTO > AddBandAsync(int bandId, int personId);
        Task<PersonWithBandsDTO >RemoveBandAsync(int bandId, int personId);


    }
}
