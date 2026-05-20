using Exceptions;
using HeavyArchives.Application.DTOs.Genres;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllGenres()
        {
            var genres = genreService.GetAllGenres();
            if (genres is null) return NotFound();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            var genre = genreService.GetGenreById(id);
            if (genre is null) return NotFound();
            return Ok(genre);
        }

        [HttpGet("ByName")]
        public IActionResult GetGenreByName(string name)
        {
            if (name is null) throw new InvalidInputException("Genre name cannot be null");
            var genre = genreService.GetGenreByName(name);
            if (genre is null) return NotFound();
            return Ok(genre);
        }

        [HttpGet("forBand")]
        public IActionResult GetGenresForSpecificBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var genres = genreService.GetGenresByBand(bandName);
            if (genres is null) return NotFound();
            return Ok(genres);
        }

        [HttpPost]
        public IActionResult CreateGenre(ManipulateGenreDTO genreDTO)
        {
            var genre = genreService.CreateGenre(genreDTO);
            return Created();
        }

        [HttpDelete]
        public IActionResult DeleteGenre(int id)
        {
            genreService.DeleteGenre(id);
            return NoContent();
        }
        [HttpPut]
        public IActionResult UpdateGenre(int id, ManipulateGenreDTO genreDTO)
        {
            var genre = genreService.UpdateGenre(id, genreDTO);
            return Ok(genre);
        }
      
    }
}
