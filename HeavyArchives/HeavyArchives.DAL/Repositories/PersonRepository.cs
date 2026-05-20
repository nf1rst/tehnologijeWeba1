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

        public ICollection<Person> GetDeceasedPeople()
        {
            return context.Persons
                .Include(p => p.PlaceOfBirth)
                .Where(p => p.DateOfDeath != null)
                .ToList();
        }

        public ICollection<Person> GetLivingPeople()
        {
            return context.Persons
                .Include(p => p.PlaceOfBirth)
                .Where(p => p.DateOfDeath == null)
                .ToList();
        }

        public ICollection<Person> GetPeopleWithDetailByCountry(string countryName)
        {
            return context.Persons
                .Include(p => p.PlaceOfBirth)
                .Where(p => p.PlaceOfBirth!=null && p.PlaceOfBirth.Name == countryName)  //jer je nullable property
                .ToList();
        }

        public ICollection<Person> GetAllPeople()
        {
            return context.Persons
                .Include(p => p.PlaceOfBirth)
                .ToList();
        }

        public ICollection<Person> GetPeopleByBand(string bandName)
        {
            return context.Persons
                .Include(p=>p.PlaceOfBirth)
                .Where(p => p.BandPerson
                    .Any(bp => bp.Band.Name==bandName))
                .ToList();
        }

        //moze se potencijalno smanjit - ovo je ono za tour manager a 
        public Person? GetPersonByFirstAndLastName(string fname, string lname)
        {
            return context.Persons
                 .Include(P => P.PlaceOfBirth)
                 .FirstOrDefault(p => p.Fname==fname &&p.Lname==lname);
        }
        
        //ovo tu se potencijano moze smanjit
        public Person? GetPersonWithDetails(int id)
        {
            return context.Persons
                .Include(p => p.BandPerson)
                    .ThenInclude(bp => bp.Band)
                .Include(P => P.PlaceOfBirth)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
