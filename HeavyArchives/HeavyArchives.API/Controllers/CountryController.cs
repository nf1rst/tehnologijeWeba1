using Exceptions;
using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Globalization;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryservice;
        public CountryController(ICountryService countryService)
        {
            this.countryservice = countryService;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = countryservice.GetAllCountries();
            return Ok(countries);// ne trea provjera jer ce uvijek vratit praznu listu ako nema niceg
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = countryservice.GetCountryById(id);
            if (country is null) return NotFound();
            return Ok(country);
        }

        [HttpGet("Name")]
        public IActionResult GetCountryByName(string countryname)
        {
            if (countryname is null) throw new InvalidInputException("Country name cannot be null");
            var country = countryservice.GetCountryByName(countryname);
            if (country is null) return NotFound();
            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry(ManipulateCountryDTO createDTO)
        {
            var country = countryservice.CreateCountry(createDTO);
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateCoutry(int id, ManipulateCountryDTO updateDTO)
        {
            var country = countryservice.UpdateCountry(id, updateDTO);
            return Ok(country);
        }

        [HttpDelete]
        public IActionResult DeleteCountry(int id)
        {
            countryservice.DeleteCountry(id);
            return NoContent();
        }
        
    }
}
