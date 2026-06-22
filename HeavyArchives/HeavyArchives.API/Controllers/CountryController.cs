using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllCountries()
        {
            var countries =await countryservice.GetAllCountriesAsync();
            return Ok(countries);// ne trea provjera jer ce uvijek vratit praznu listu ako nema niceg
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country =await countryservice.GetCountryByIdAsync(id);
            if (country is null) return NotFound();
            return Ok(country);
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetCountryByName(string countryname)
        {
            if (countryname is null) throw new InvalidInputException("Name cannot be null");

            var country =await countryservice.GetCountryByNameAsync(countryname);
            if (country is null) return NotFound();
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(ManipulateCountryDTO createDTO)
        {
            var country =await countryservice.CreateCountryAsync(createDTO);
            return CreatedAtAction(nameof(GetCountryById), new {id=country.Id}, country);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoutry(int id, ManipulateCountryDTO updateDTO)
        {
            var country =await countryservice.UpdateCountryAsync(id, updateDTO);
            return Ok(country);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await countryservice.DeleteCountryAsync(id);
            return NoContent();
        }
        
    }
}
