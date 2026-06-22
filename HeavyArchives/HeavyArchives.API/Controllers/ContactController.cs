using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Contact;
using HeavyArchives.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;
        private readonly IValidator<ManipulateContactDTO> contactValidator;

        public ContactController(IContactService contactService, IValidator<ManipulateContactDTO> contactValidator)
        {
            this.contactService = contactService;
            this.contactValidator = contactValidator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(ManipulateContactDTO createDTO)
        {
            var contact =await contactService.CreateContactAsync(createDTO);
            var res = await contactValidator.ValidateAsync(createDTO);
            if (!res.IsValid)
            {
                string message = string.Join(", ", res.Errors);
                throw new InvalidInputException(message);
            }
            return CreatedAtAction(nameof(GetContactForBand), new {id=contact.Id}, contact);
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
