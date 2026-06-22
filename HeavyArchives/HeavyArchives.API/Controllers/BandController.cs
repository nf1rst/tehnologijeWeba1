using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Band;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAll()
        {
            var bands =await bandService.GetAllBandsAsync();
            if (bands is null) return NotFound();
            return Ok(bands);
        }


        [HttpGet("byName")] 
        public async Task<IActionResult> GetBandByName(string name)
        {
            if (name is null) throw new InvalidInputException("Name cannot be null");
            var band =await bandService.GetBandByNameAsync(name);
            if(band is null)return NotFound();
            return Ok(band);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)// treba vratit i members
        {
            var target =await bandService.GetBandByIdAsync(id);
            return Ok(target);
        }

        [HttpGet("byCountry")]
        public async Task<IActionResult> GetBandByCountry(string countryName) //kriva nomenklatura vraca vise bendova ne jedan
        {
            if (countryName is null) throw new InvalidInputException("Country name cannot be null");
            var target =await bandService.GetBandsByCountryAsync(countryName);
            return Ok(target);
        }
    

        [HttpGet("Genre")]
        public async Task<IActionResult> GetBandByGenre(string genreName)
        {
            if (genreName is null) throw new InvalidInputException("Genre name cannot be null");

            var bands =await bandService.GetBandsByGenreAsync(genreName);
            if (bands is null) return NotFound();
            return Ok(bands);
        }

        [HttpGet("Active")]
        public async Task<IActionResult> GetActiveBands()
        {
            var bands =await bandService.GetActiveBandsAsync();
            return Ok(bands); 
        }


        [HttpPost]
        public async Task<IActionResult> CreateBand(ManipulateBandDTO bandDTO)
        {
            var band =await bandService.CreateBandAsync(bandDTO);
            return CreatedAtAction(nameof(GetById), new {id=band.Id}, band);
        }

        [HttpPut("AddMember")]
        public async Task<IActionResult> AddBandMember(int bandId, int personId,string status, string role) // status vise nije enum pa samo string ovdje prima
        {
            if (status is null) throw new InvalidInputException("Status cannot be null");
            if (role is null) throw new InvalidInputException("Role cannot be null");

            var result =await bandService.AddMemberAsync(bandId, personId, status, role);
            return Ok(result); 
        }

        [HttpPut("RemoveMember")]
        public async Task<IActionResult> RemoveBandMember(int bandId,int memberId)
        {
            var result =await bandService.RemoveMemberAsync(bandId, memberId);
            return Ok(result);
        }

        [HttpPut("AddGenre")]
        public async Task<IActionResult> AddBandGenre(int bandId, int genreId)
        {
            var result =await bandService.AddGenreAsync(bandId, genreId);
            return Ok(result);
        }
        [HttpPut("RemoveGenre")]
        public async Task<IActionResult> RemoveBandGenre(int bandId, int genreId)
        {
            var result =await bandService.RemoveGenreAsync(bandId, genreId);
            return Ok(result);
        }
        [HttpPut("UpdateBand")]
        public async Task<IActionResult> UpdateBand(int id, ManipulateBandDTO updatedDTO)
        {
            var result =await bandService.UpdateBandAsync(id, updatedDTO);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBand(int id)
        {
            await bandService.DeleteBandAsync(id);
            return NoContent();
        }

        [HttpGet("bands/{personId}")]
        public async Task<IActionResult> GetBandsForPerson(int personId)
        {
            var bands = await bandService.GetBandsForPersonByPersonId(personId);
            return Ok(bands);
        }
    }

}
