using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.BandMemberStatus;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class BandMemberStatusController : ControllerBase
    {
        private readonly IBandMemberStatusService bandMemberStatusService;

        public BandMemberStatusController(IBandMemberStatusService bandMemberStatusService, IValidator<String>validator)
        {
            this.bandMemberStatusService = bandMemberStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBandMemberStatuses()
        {
            var statuses = await bandMemberStatusService.GetAllStatusesAsync();
            return Ok(statuses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBandMemberStatus(ManipulateBandMemberStatusDTO createDTO)
        {

            var created = await bandMemberStatusService.CreateBandMemberStatusAsync(createDTO);
            return Ok(created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBandMemberStatus(int id)
        {
            await bandMemberStatusService.DeleteBandMemberStatusAsync(id);
            return NoContent();
        }
    }
}
