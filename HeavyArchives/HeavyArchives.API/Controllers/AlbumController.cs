using Exceptions;
using HeavyArchives.Application.DTOs.Album;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : BASEController<AlbumController>
    {
        private readonly IAlbumService albumService;
        public AlbumController(IAlbumService albumService, ILogger<AlbumController>logger):base(logger) // prosljeduje to u konstruktor od BASECOntrollera
        {
            this.albumService = albumService;
        }

        //Log metoda mora bit iznad returnova inace se nista nece zapisat 

        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums =await albumService.GetAllAlbumsAsync();
            LogInfo(nameof(GetAlbums));

            if (albums is null) return NotFound();
            return Ok(albums);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(CreateAlbumDTO albumDTO)
        {
            var album =await albumService.CreateAlbumAsync(albumDTO);
            LogInfo(nameof(CreateAlbum));
            if (album is null) return BadRequest();
            return CreatedAtAction(nameof(CreateAlbum), new { Id = album.Id }, album);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            var album =await albumService.GetAlbumByIdAsync(id);
            LogInfo(nameof(GetAlbumById));
            if (album is null) return NotFound();
            return Ok(album);
        }

        [HttpGet("[action]")] // ovo je losa oznaka, koja promijeni u nesto bolje 
        public async Task<IActionResult> GetAlbumsByBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var albums =await albumService.GetAlbumsByBandAsync(bandName);
            LogInfo(nameof(GetAlbumsByBand));
            return Ok(albums);
        }
    
        [HttpGet("Year")]
        public async Task<IActionResult> GetAlbumsByYear(int year)
        {
            var albums =await albumService.GetAlbumsByYearAsync(year);
            LogInfo(nameof(GetAlbumById));
            return Ok(albums); //nece nikad vratit null zbog ToList u repo, vraca se prazna lista ne null
        }
        [HttpGet("ByType")]
        public async Task<IActionResult> GetAlbumsByType(string type)
        {
            if (type is null) throw new InvalidInputException("Type cannot be null");
            var albums =await albumService.GetAlbumsByTypeAsync(type);
            LogInfo(nameof(GetAlbumsByType));
            return Ok(albums);
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetAlbumByName(string name)
        {
            if (name is null) throw new InvalidInputException("Album name cannot be null");
            var album =await albumService.GetAlbumByNameAsync(name);
            LogInfo(nameof(GetAlbumByName));
            if (album is null) return NotFound();
            return Ok(album);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            LogInfo(nameof(DeleteAlbum));
            await albumService.DeleteAlbumAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAlbum(int id, UpdateAlbuumDTO albumDTO)
        {
            // ako ga pronade neka bude no content
            LogInfo(nameof(UpdateAlbum));
            var update =await albumService.UpdateAlbumAsync(id, albumDTO);
            return Ok(update);
        }

    }
}
