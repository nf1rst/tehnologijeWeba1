using HeavyArchives.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface IContactService
    {
        Task<ContactDTO> GetContactForBandAsync(string bandName);

        Task<ContactDTO> CreateContactAsync(ManipulateContactDTO createDTO);
        Task<ContactDTO > UpdateContactAsync(string bandName, UpdateContactDTO updateContactDTO);
        Task DeleteContactAsync(string bandName); // kontat ce se brisat po bendu
    }
}
