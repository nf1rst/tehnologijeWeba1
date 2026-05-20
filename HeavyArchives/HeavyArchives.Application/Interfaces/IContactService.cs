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
        ContactDTO GetContactForBand(string bandName);

        ContactDTO CreateContact(ManipulateContactDTO createDTO);
        ContactDTO UpdateContact(string bandName, ManipulateContactDTO updateContactDTO);
        void DeleteContact(string bandName); // kontat ce se brisat po bendu
    }
}
