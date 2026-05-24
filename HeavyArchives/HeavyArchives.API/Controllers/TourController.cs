using Exceptions;
using HeavyArchives.Application.DTOs.Tour;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourService tourService;
        public TourController(ITourService tourService)
        {
            this.tourService= tourService;
        }
        [HttpGet]
        public IActionResult GetAllTours()
        {
            var tours = tourService.GetAllToursAsync();
            return Ok(tours);
        }

        [HttpGet("Manager/{id}")]
        public async Task<IActionResult> GetToursByManager(string manager)
        {
            if (manager is null) throw new InvalidInputException("Manager name name cannot be null");
            var tours =await tourService.GetAllToursByManagerAsync(manager);
            return Ok(tours);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetTourByName([FromRoute]string name)
        {
            if (name is null) throw new InvalidInputException("Tour name cannot be null");
            var tour =await tourService.GetTourByNameAsync(name);
            return Ok(tour);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTour(int id)
        {
            await tourService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTour(ManipulateTourDTO createDTO)
        {
            var tour =await tourService.CreateTourAsync(createDTO);
            return Ok(tour);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTour(int id, ManipulateTourDTO updateDTO)
        {
            var tour =await tourService.UpdateTourAsync(id, updateDTO);
            return Ok(tour);
        }
    }
}
