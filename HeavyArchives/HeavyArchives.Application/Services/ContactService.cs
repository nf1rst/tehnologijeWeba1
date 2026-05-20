using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Contact;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;

namespace HeavyArchives.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public ContactService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow=uow;
            this.mapper= mapper;
        }

        public ContactDTO CreateContact(ManipulateContactDTO createDTO)
        {
            Band? band = uow.Bands.GetBandWithDetailsByName(createDTO.Band);
            if (band is null) throw new EntityNotExistant();


            Contact contact = new Contact
            {
                Tel = createDTO.Tel,
                Website = createDTO.Website,
                Email = createDTO.Email,
                BandId=band.Id
            };
            uow.Contacts.Insert(contact);
            uow.SaveChanges();
            return mapper.Map<ContactDTO>(contact);
        }

        public void DeleteContact(string bandName)
        {
            Contact? contact = uow.Contacts.GetContactForBand(bandName);
            if (contact is null) throw new Exceptions.EntityNotExistant("Contact wasn't found");

            uow.Contacts.Delete(contact);
            uow.SaveChanges();
        }

        public ContactDTO GetContactForBand(string bandName)
        {
            var contact = uow.Contacts.GetContactForBand(bandName);
            return mapper.Map<ContactDTO>(contact);
        }

        public ContactDTO UpdateContact(string bandName, ManipulateContactDTO updateContactDTO)
        {
            Contact? contact = uow.Contacts.GetContactForBand(bandName);
            if (contact is null) throw new EntityNotExistant("Contact wasn't found");

            mapper.Map(updateContactDTO, contact);

            uow.SaveChanges();
            return mapper.Map<ContactDTO>(contact);
        }
    }
}
