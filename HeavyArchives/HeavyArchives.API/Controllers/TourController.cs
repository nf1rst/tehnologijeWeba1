using Exceptions;
using HeavyArchives.Application.DTOs.Tour;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var tours = tourService.GetAllTours();
            return Ok(tours);
        }

        [HttpGet("Manager/{id}")]
        public IActionResult GetToursByManager(string manager)
        {
            if (manager is null) throw new InvalidInputException("Manager name name cannot be null");
            var tours = tourService.GetAllToursByManager(manager);
            return Ok(tours);
        }
        [HttpGet("{name}")]
        public IActionResult GetTourByName([FromRoute]string name)
        {
            if (name is null) throw new InvalidInputException("Tour name cannot be null");
            var tour = tourService.GetTourByName(name);
            return Ok(tour);
        }
        [HttpDelete]
        public IActionResult DeleteTour(int id)
        {
            tourService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        public IActionResult CreateTour(ManipulateTourDTO createDTO)
        {
            var tour = tourService.CreateTour(createDTO);
            return Ok(tour);
        }
        [HttpPut]
        public IActionResult UpdateTour(int id, ManipulateTourDTO updateDTO)
        {
            var tour = tourService.UpdateTour(id, updateDTO);
            return Ok(tour);
        }
    }
}
