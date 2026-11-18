using EncyclopaediaBisemutum.Application.DTOs.City;
using EncyclopaediaBisemutum.Application.Interfaces;
using HeavyArchives.API.Controllers;
using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.Services;
using HeavyArchives.DAL.Repositories;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly ILogger<CityController> logger;

        public CityController(ICityService cityService, ILogger<CityController> logger)
        {
            this.cityService = cityService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await cityService.GetAllCities();
            return Ok(cities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            var city = await cityService.GetCityById(id);
            return Ok(city);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCityByName(string name)
        {
            var city = await cityService.GetCityByName(name);
            return Ok(city);
        }

        [HttpGet("country/{coutnryName}")]
        public async Task<IActionResult> GetCitiesByCountry(string coutnryName)
        {
            var cities = await cityService.GetCitiesByCountry(coutnryName);
            return Ok(cities);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(ManipulateCityDTO createDTO)
        {
            var create = await cityService.CreateCity(createDTO);
            logger.LogInformation("[{Controller}] City created: {@City}", nameof(CityController), create);
            return Ok(create);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await cityService.DeleteCity(id);
            logger.LogInformation("[{Controller}] City deleted: {@City}", nameof(CityController), id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity(int id, ManipulateCityDTO updateDTO)
        {
            var updated = await cityService.UpdateCity(id, updateDTO);
            return Ok(updated);
        }
    }

}
