using Exceptions;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertController : BASEController<ConcertController>
    {
        private readonly IConcertService concertService;
        public ConcertController(IConcertService concertService, ILogger<ConcertController> logger):base(logger)
        {
            this.concertService = concertService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllConcerts()
        {
            var concerts =await concertService.GetAllConcertsAsync();
            return Ok(concerts);
        }

        [HttpGet("/Tour/{tourName}")]
        public async Task<IActionResult> GetConcertsForTour( [FromRoute]string tourName)
        {
            if (tourName is null) throw new InvalidInputException("Tour name cannot be null");
            var concerts =await concertService.GetConcertsForTourAsync(tourName);
            return Ok(concerts);
        }

        [HttpGet("/Country/{countryName}")]
        public async Task<IActionResult> GetConcertsForCountry( [FromRoute]string countryName){
            if (countryName is null) throw new InvalidInputException("Country name cannot be null");
            var concerts =await concertService.GetConcertsForCountryAsync(countryName);
            return Ok(concerts);
        }

        [HttpGet("/City/{cityName}")]
        public async Task<IActionResult> GetConcertsForCity([FromRoute]string cityName){
            if (cityName is null) throw new InvalidInputException("City name cannot be null");
            var concerts =await concertService.GetConcertsForCityAsync(cityName);
            return Ok(concerts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConcert(ManipulateConcertDTO createDTO)
        {

            var created =await concertService.CreateConcertAsync(createDTO);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateConcert(int id, ManipulateConcertDTO updateDTO)
        {
            var updated =await concertService.UpdateConcertAsync(id, updateDTO);
            return Ok(updated);
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteConcert(int id)
        {
            await concertService.DeleteConcertAsync(id);
            return NoContent();
        }
    }
}
