using EncyclopaediaBisemutum.Application.DTOs.City;
using EncyclopaediaBisemutum.Application.Interfaces;
using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
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
            return Ok(create);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await cityService.DeleteCity(id);
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
