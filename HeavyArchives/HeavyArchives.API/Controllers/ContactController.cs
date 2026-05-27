using Exceptions;
using HeavyArchives.Application.DTOs.Contact;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(ManipulateContactDTO createDTO)
        {
            var contact =await contactService.CreateContactAsync(createDTO);
            return Ok(contact);
        }

        [HttpGet]
        public async Task<IActionResult> GetContactForBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var contact =await contactService.GetContactForBandAsync(bandName);
            if (contact is null) return NotFound();
            return Ok(contact);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateContact(string bandName, UpdateContactDTO updsateDTO)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var contact =await contactService.UpdateContactAsync(bandName, updsateDTO);
            return Ok(contact);
        }
    }
}
