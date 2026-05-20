using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HeavyArchives.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CountryService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public CountryDTO CreateCountry(ManipulateCountryDTO createDTO)
        {
            Country country = mapper.Map<Country>(createDTO);
            uow.Countries.Insert(country);
            uow.SaveChanges();
            return mapper.Map<CountryDTO>(country);
        }

        public void DeleteCountry(int id)
        {
            Country? country = uow.Countries.GetById(id);
            if (country is null)throw new EntityNotExistant("Country wasn't found");
            uow.Countries.Delete(country);
            uow.SaveChanges();
        }

        public ICollection<CountryDTO> GetAllCountries()
        {
            var countries = uow.Countries.GetAllCountries();
            return mapper.Map<ICollection<CountryDTO>>(countries);
        }

        public CountryDTO? GetCountryById(int id)
        {
            Country? country = uow.Countries.GetCountryById(id);
            if (country is null) throw new EntityNotExistant("Country wasn't found");
            return mapper.Map<CountryDTO>(country);
        }

        public CountryDTO? GetCountryByName(string name)
        {
            Country? country = uow.Countries.GetCountryByName(name);
            if(country is null) throw new EntityNotExistant("Country wasn't found");
            return mapper.Map<CountryDTO>(country);
        }

        public CountryDTO UpdateCountry(int id, ManipulateCountryDTO updatedDTO)
        {
            Country? country = uow.Countries.GetCountryById(id);
            if (country is null) throw new EntityNotExistant("Country wasn't found");

            mapper.Map(updatedDTO, country);
            uow.SaveChanges();
            return mapper.Map<CountryDTO>(country);
        }
    }
}
