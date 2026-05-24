using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Contact;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System.Threading.Tasks;

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

        public async Task<ContactDTO> CreateContactAsync(ManipulateContactDTO createDTO)
        {
            Band? band =await uow.Bands.GetBandWithDetailsByNameAsync(createDTO.Band);
            if (band is null) throw new EntityNotExistant();


            Contact contact = new Contact
            {
                Tel = createDTO.Tel,
                Website = createDTO.Website,
                Email = createDTO.Email,
                BandId=band.Id
            };
            await uow.Contacts.InsertAsync(contact);
            await uow.SaveChangesAsync();
            return mapper.Map<ContactDTO>(contact);
        }

        public async Task DeleteContactAsync(string bandName)
        {
            Contact? contact =await uow.Contacts.GetContactForBandAsync(bandName);
            if (contact is null) throw new Exceptions.EntityNotExistant("Contact wasn't found");

            await uow.Contacts.DeleteAsync(contact);
            await uow.SaveChangesAsync();
        }

        public async Task<ContactDTO> GetContactForBandAsync(string bandName)
        {
            var contact =await uow.Contacts.GetContactForBandAsync(bandName);
            return mapper.Map<ContactDTO>(contact);
        }

        public async Task<ContactDTO> UpdateContactAsync(string bandName, ManipulateContactDTO updateContactDTO)
        {
            Contact? contact =await uow.Contacts.GetContactForBandAsync(bandName);
            if (contact is null) throw new EntityNotExistant("Contact wasn't found");

            mapper.Map(updateContactDTO, contact);

            await uow.SaveChangesAsync();
            return mapper.Map<ContactDTO>(contact);
        }
    }
}
