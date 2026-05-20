using Exceptions;
using HeavyArchives.Application.DTOs.Album;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public IActionResult GetAlbums()
        {
            var albums = albumService.GetAllAlbums();
            LogInfo(nameof(GetAlbums));

            if (albums is null) return NotFound();
            return Ok(albums);
        }

        [HttpPost]
        public IActionResult CreateAlbum(CreateAlbumDTO albumDTO)
        {
            var album = albumService.CreateAlbum(albumDTO);
            LogInfo(nameof(CreateAlbum));
            if (album is null) return BadRequest();
            return CreatedAtAction(nameof(CreateAlbum), new { Id = album.Id }, album);
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            var album = albumService.GetAlbumById(id);
            LogInfo(nameof(GetAlbumById));
            if (album is null) return NotFound();
            return Ok(album);
        }

        [HttpGet("[action]")] // ovo je losa oznaka, koja promijeni u nesto bolje 
        public IActionResult GetAlbumsByBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var albums = albumService.GetAlbumsByBand(bandName);
            LogInfo(nameof(GetAlbumsByBand));
            return Ok(albums);
        }
    
        [HttpGet("Year")]
        public IActionResult GetAlbumsByYear(int year)
        {
            var albums = albumService.GetAlbumsByYear(year);
            LogInfo(nameof(GetAlbumById));
            return Ok(albums); //nece nikad vratit null zbog ToList u repo, vraca se prazna lista ne null
        }
        [HttpGet("ByType")]
        public IActionResult GetAlbumsByType(string type)
        {
            if (type is null) throw new InvalidInputException("Type cannot be null");
            var albums = albumService.GetAlbumsByType(type);
            LogInfo(nameof(GetAlbumsByType));
            return Ok(albums);
        }

        [HttpGet("Name")]
        public IActionResult GetAlbumByName(string name)
        {
            if (name is null) throw new InvalidInputException("Album name cannot be null");
            var album = albumService.GetAlbumByName(name);
            LogInfo(nameof(GetAlbumByName));
            if (album is null) return NotFound();
            return Ok(album);
        }


        [HttpDelete]
        public IActionResult DeleteAlbum(int id)
        {
            LogInfo(nameof(DeleteAlbum));
            albumService.DeleteAlbum(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateAlbum(int id, UpdateAlbuumDTO albumDTO)
        {
            // ako ga pronade neka bude no content
            LogInfo(nameof(UpdateAlbum));
            var update = albumService.UpdateAlbum(id, albumDTO);
            return Ok(update);
        }

    }
}
