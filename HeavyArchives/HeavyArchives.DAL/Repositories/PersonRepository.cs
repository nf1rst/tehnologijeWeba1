using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        protected readonly HeavyArchivesContext context;

        public PersonRepository(HeavyArchivesContext context):base(context)
        {
            this.context = context;
        }

        public async Task<ICollection<Person>> GetPeopleByBandAsync(string bandName)
        {
            return await context.Persons
                .Include(p => p.PlaceOfBirth)
                .Where(p => p.BandPerson
                    .Any(bp => bp.Band.Name == bandName))
                .ToListAsync();
        }

        public async Task<ICollection<Person>> GetAllPeopleASync()
        {
            return await context.Persons
                .Include(p => p.PlaceOfBirth)
                .ToListAsync();
        }

        public async Task<ICollection<Person>> GetDeceasedPeopleAsync()
        {
            return await context.Persons
                .Include(p => p.PlaceOfBirth)
                .Where(p => p.DateOfDeath != null)
                .ToListAsync();
        }

        public async Task<ICollection<Person>> GetLivingPeopleAsync()
        {
            return  await context.Persons
                .Include(p => p.PlaceOfBirth)
                .Where(p => p.DateOfDeath == null)
                .ToListAsync();
        }

        public async Task<ICollection<Person>> GetPeopleWithDetailByCountryAsync(string countryName)
        {
            return await context.Persons
                .Include(p => p.PlaceOfBirth)
                .Where(p => p.PlaceOfBirth != null && p.PlaceOfBirth.Name == countryName)  //jer je nullable property
                .ToListAsync();
        }

        public async Task<Person?> GetPersonWithDetailsAsync(int id)
        {
            return await context.Persons
                .Include(p => p.BandPerson)
                    .ThenInclude(bp => bp.Band)
                .Include(P => P.PlaceOfBirth)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person?> GetPersonByFirstAndLastNameAsync(string fname, string lname)
        {
            return await context.Persons
                 .Include(P => P.PlaceOfBirth)
                 .FirstOrDefaultAsync(p => p.Fname == fname && p.Lname == lname);
        }
    }
}
