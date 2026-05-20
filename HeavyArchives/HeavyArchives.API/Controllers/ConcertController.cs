using Exceptions;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllConcerts()
        {
            var concerts = concertService.GetAllConcerts();
            return Ok(concerts);
        }

        [HttpGet("/Tour/{tourName}")]
        public IActionResult GetConcertsForTour( [FromRoute]string tourName)
        {
            if (tourName is null) throw new InvalidInputException("Tour name cannot be null");
            var concerts = concertService.GetConcertsForTour(tourName);
            return Ok(concerts);
        }

        [HttpGet("/Country/{countryName}")]
        public IActionResult GetConcertsForCountry( [FromRoute]string countryName){
            if (countryName is null) throw new InvalidInputException("Country name cannot be null");
            var concerts = concertService.GetConcertsForCountry(countryName);
            return Ok(concerts);
        }

        [HttpGet("/City/{cityName}")]
        public IActionResult GetConcertsForCity([FromRoute]string cityName){
            if (cityName is null) throw new InvalidInputException("City name cannot be null");
            var concerts = concertService.GetConcertsForCity(cityName);
            return Ok(concerts);
        }

        [HttpPost]
        public IActionResult CreateConcert(ManipulateConcertDTO createDTO)
        {

            var created = concertService.CreateConcert(createDTO);
            return Created();
        }
        [HttpPut]
        public IActionResult UpdateConcert(int id, ManipulateConcertDTO updateDTO)
        {
            var updated = concertService.UpdateConcert(id, updateDTO);
            return Ok(updated);
        }

        [HttpDelete]

        public IActionResult DeleteConcert(int id)
        {
            concertService.DeleteConcert(id);
            return NoContent();
        }
    }
}
