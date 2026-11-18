using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConcertController : ControllerBase
    {
        private readonly IConcertService concertService;
        private readonly ILogger<ConcertController> logger;
        public ConcertController(IConcertService concertService, ILogger<ConcertController> logger)
        {
            this.concertService = concertService;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllConcerts()
        {
            var concerts =await concertService.GetAllConcertsAsync();
            return Ok(concerts);
        }
        [HttpPatch("{concertId}/tickets")]
        public async Task<IActionResult> PurchaseTicket(int concertId)
        {
            ConcertDTO? concert=await concertService.SellTicketAsync(concertId);
            return Ok(concert);
        }

        [HttpGet("Tour/{tourName}")]
        public async Task<IActionResult> GetConcertsForTour( [FromRoute]string tourName)
        {
            if (tourName is null) throw new InvalidInputException("Tour name cannot be null");

            var concerts =await concertService.GetConcertsForTourAsync(tourName);
            return Ok(concerts);
        }

        [HttpGet("Country/{countryName}")]
        public async Task<IActionResult> GetConcertsForCountry( [FromRoute]string countryName){
            if (countryName is null) throw new InvalidInputException("Country name cannot be null");

            var concerts =await concertService.GetConcertsForCountryAsync(countryName);
            return Ok(concerts);
        }

        [HttpGet("City/{cityName}")]
        public async Task<IActionResult> GetConcertsForCity([FromRoute]string cityName){
            if (cityName is null) throw new InvalidInputException("City name cannot be null");

            var concerts =await concertService.GetConcertsForCityAsync(cityName);
            return Ok(concerts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetConcertById(int id)
        {
            var concert = await concertService.GetConcertByIdAsyncAsync(id);
            return Ok(concert);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConcert(ManipulateConcertDTO createDTO)
        {

            var created =await concertService.CreateConcertAsync(createDTO);
            logger.LogInformation("[{Controller}] Concert created: {@Concert}", nameof(ConcertController), created);
            return CreatedAtAction(nameof(GetConcertById), new { id=created.Id}, created);
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
