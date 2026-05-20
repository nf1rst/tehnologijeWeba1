using Exceptions;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            var people = personService.GetAllPersons();
            if (people is null) return NotFound();
            return Ok(people);
        }


        [HttpGet("ByBand")]
        public IActionResult GetPeopleByBandName(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var people = personService.GetPeopleByBand(bandName);
            if (people is null) return NotFound();
            return Ok(people);
        }

        [HttpGet("Alive")]
        public IActionResult GetLivingPeople()
        {
            var people = personService.GetLivingPeople();
            if (people is null) return NotFound();
            return Ok(people);
        }
        [HttpGet("Deceased")]
        public IActionResult GetDeadPeople()
        {
            var people = personService.GetDeceasedPeople();
            if (people is null) return NotFound();
            return Ok(people);
        }
        [HttpGet("ByCountry")]
        public IActionResult GetPeopleByCountry(string countryName)
        {
            if (countryName is null) throw new InvalidInputException("Country name cannot be null");
            var people = personService.GetPeopleByCountry(countryName);
            if (people is null) return NotFound();
            return Ok(people);
        }

        [HttpPut("UpdatePerson")]
        public IActionResult UpdatePerson(int id, ManipulatePersonDTO updatePersonDTO)
        {
            var person = personService.UpdatePerson(id, updatePersonDTO);
            return Ok(person);
        }

        [HttpPost]
        public IActionResult CreatePerson(ManipulatePersonDTO personDTO)
        {
            var created = personService.CreatePerson(personDTO);

            if (created is null) return BadRequest("User information given was wrong");

            return Ok(created);

        }

        [HttpDelete]
        public IActionResult DeletePerson(int id)
        {
            personService.DeletePerson(id);
            return NoContent();
        }

    }
}
