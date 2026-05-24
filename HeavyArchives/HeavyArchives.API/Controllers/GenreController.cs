using Exceptions;
using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;
        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres =await genreService.GetAllGenresAsync();
            if (genres is null) return NotFound();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre =await genreService.GetGenreByIdAsync(id);
            if (genre is null) return NotFound();
            return Ok(genre);
        }

        [HttpGet("ByName")]
        public async Task<IActionResult> GetGenreByName(string name)
        {
            if (name is null) throw new InvalidInputException("Genre name cannot be null");
            var genre =await genreService.GetGenreByNameAsync(name);
            if (genre is null) return NotFound();
            return Ok(genre);
        }

        [HttpGet("forBand")]
        public async Task<IActionResult> GetGenresForSpecificBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var genres =await genreService.GetGenresByBandAsync(bandName);
            if (genres is null) return NotFound();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(ManipulateGenreDTO genreDTO)
        {
            var genre =await genreService.CreateGenreAsync(genreDTO);
            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await genreService.DeleteGenreAsync(id);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGenre(int id, ManipulateGenreDTO genreDTO)
        {
            var genre =await genreService.UpdateGenreAsync(id, genreDTO);
            return Ok(genre);
        }
      
    }
}
