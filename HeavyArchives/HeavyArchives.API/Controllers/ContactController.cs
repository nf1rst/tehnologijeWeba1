using Exceptions;
using HeavyArchives.Application.DTOs.Contact;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateContact(ManipulateContactDTO createDTO)
        {
            var contact = contactService.CreateContact(createDTO);
            return Ok(contact);
        }

        [HttpGet]
        public IActionResult GetContactForBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var contact = contactService.GetContactForBand(bandName);
            if (contact is null) return NotFound();
            return Ok(contact);
        }
        [HttpPut]
        public IActionResult UpdateContact(string bandName, ManipulateContactDTO updsateDTO)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var contact = contactService.UpdateContact(bandName, updsateDTO);
            return Ok(contact);
        }
    }
}
