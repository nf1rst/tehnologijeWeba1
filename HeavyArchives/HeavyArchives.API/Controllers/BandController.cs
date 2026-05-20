using Exceptions;
using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.NetworkInformation;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {
        private readonly IBandService bandService;
        public BandController(IBandService bandService)
        {
            this.bandService = bandService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bands = bandService.GetAllBands();
            if (bands is null) return NotFound();
            return Ok(bands);
        }


        [HttpGet("byName")] // treba vratit i members
        [Authorize(Roles ="User,Admin")]

        public IActionResult GetBandByName(string name)
        {
            if (name is null) throw new InvalidInputException("Band name cannot be null");
            var band = bandService.GetBandByName(name);
            if(band is null)return NotFound();
            return Ok(band);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)// treba vratit i members
        {
            var target = bandService.GetBandById(id);
            if (target is null) return NotFound();
            return Ok(target);
        }

        [HttpGet("byCountry")]
        public IActionResult GetBandByCountry(string countryName) //kriva nomenklatura vraca vise bendova ne jedan
        {
            if (countryName is null) throw new InvalidInputException("Country name cannot be null");
            var target = bandService.GetBandsByCountry(countryName);
            if (target is null) return NotFound();
            return Ok(target);
        }

        [HttpGet("Genre")]
        public IActionResult GetBandByGenre(string genreName)
        {
            if (genreName is null) throw new InvalidInputException("Genre name cannot be null");
            var bands = bandService.GetBandsByGenre(genreName);
            if (bands is null) return NotFound();
            return Ok(bands);
        }

        [HttpGet("Active")]
        public IActionResult GetActiveBands()
        {
            var bands = bandService.GetActiveBands();
            return Ok(bands); 
        }


        [HttpPost]
        public IActionResult CreateBand(ManipulateBandDTO bandDTO)
        {
            var band = bandService.CreateBand(bandDTO);
            return CreatedAtAction(nameof(GetById), new {id=band.Id}, band);
        }

        [HttpPut("AddMember")]
        public IActionResult AddBandMember(int bandId, int personId,string status, string role) // status vise nije enum pa samo string ovdje prima
        {
            if (status is null) throw new InvalidInputException("Status cannot be null");
            if (role is null) throw new InvalidInputException("Role cannot be null");
            var result = bandService.AddMember(bandId, personId, status, role);
            return Ok(result); 
            //inace je bad request jer nesto ne postoji 
        }

        [HttpPut("RemoveMember")]
        public IActionResult RemoveBandMember(int bandId,int memberId)
        {
            var result = bandService.RemoveMember(bandId, memberId);
            return Ok(result);
        }

        [HttpPut("AddGenre")]
        public IActionResult AddBandGenre(int bandId, int genreId)
        {
            var result = bandService.AddGenre(bandId, genreId);
            return Ok(result);
        }
        [HttpPut("RemoveGenre")]
        public IActionResult RemoveBandGenre(int bandId, int genreId)
        {
            var result = bandService.RemoveGenre(bandId, genreId);
            return Ok(result);
        }
        [HttpPut("UpdateBand")]
        public IActionResult UpdateBand(int id, ManipulateBandDTO updatedDTO)
        {
            var result = bandService.UpdateBand(id, updatedDTO);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteBand(int id)
        {
            bandService.DeleteBand(id);
            return NoContent();
        }
    }

}
