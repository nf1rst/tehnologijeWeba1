using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IValidator<ManipulatePersonDTO> validator;
        public PersonController(IPersonService personService, IValidator<ManipulatePersonDTO> validator)
        {
            this.personService = personService;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var people =await personService.GetAllPersonsAsync();
            if (people is null) return NotFound();
            return Ok(people);
        }


        [HttpGet("ByBand")]
        public async Task<IActionResult> GetPeopleByBandName(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Name cannot be null");

            var people =await personService.GetPeopleByBandAsync(bandName);
            if (people is null) return NotFound();
            return Ok(people);
        }

        [HttpGet("Alive")]
        public async Task<IActionResult> GetLivingPeople()
        {
            var people =await personService.GetLivingPeopleAsync();
            if (people is null) return NotFound();
            return Ok(people);
        }
        [HttpGet("Deceased")]
        public async Task<IActionResult> GetDeadPeople()
        {
            var people =await personService.GetDeceasedPeopleAsync();
            if (people is null) return NotFound();
            return Ok(people);
        }
        [HttpGet("ByCountry")]
        public async Task<IActionResult> GetPeopleByCountry(string countryName)
        {
            if (countryName is null) throw new InvalidInputException("Name cannot be null");

            var people =await personService.GetPeopleByCountryAsync(countryName);
            if (people is null) return NotFound();
            return Ok(people);
        }

        [HttpPut("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson(int id, ManipulatePersonDTO updatePersonDTO)
        {
            var person =await personService.UpdatePersonAsync(id, updatePersonDTO);
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(ManipulatePersonDTO personDTO)
        {
            //https://oneuptime.com/blog/post/2026-01-25-fluentvalidation-dotnet/view
            var val = await validator.ValidateAsync(personDTO);
            if (!val.IsValid)
            {
                string message = string.Join(",", val.Errors.Select(e=>e.ErrorMessage));
                throw new InvalidInputException(message);
            }
            var created =await personService.CreatePersonAsync(personDTO);

            if (created is null) return BadRequest("User information given was wrong");

            return Ok(created);

        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int id)
        {
           await  personService.DeletePersonAsync(id);
            return NoContent();
        }

    }
}
