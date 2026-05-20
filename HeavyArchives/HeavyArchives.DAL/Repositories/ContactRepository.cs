using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeavyArchives.DAL.Repositories
{
    public class ContactRepository:BaseRepository<Contact>, IContactRepository
    {
        private readonly HeavyArchivesContext context;
        public ContactRepository(HeavyArchivesContext context):base(context)
        {
            this.context = context;
        }

        public Contact? GetContactForBand(string bandName)
        {
            return context.Contacts
                .Include(c=>c.Band)
                .FirstOrDefault(c => c.Band.Name == bandName); // include band?
        }
    }
}
