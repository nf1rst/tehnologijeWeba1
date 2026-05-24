using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Country;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

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
        public async Task<CountryDTO> CreateCountryAsync(ManipulateCountryDTO createDTO)
        {
            Country country = mapper.Map<Country>(createDTO);
            await uow.Countries.InsertAsync(country);
            await uow.SaveChangesAsync();
            return mapper.Map<CountryDTO>(country);
        }

        public async Task DeleteCountryAsync(int id)
        {
            Country? country =await uow.Countries.GetByIdAsync(id);
            if (country is null)throw new EntityNotExistant("Country wasn't found");
            await uow.Countries.DeleteAsync(country);
            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<CountryDTO>> GetAllCountriesAsync()
        {
            var countries =await uow.Countries.GetAllCountriesAsync();
            return mapper.Map<ICollection<CountryDTO>>(countries);
        }

        public async Task<CountryDTO?> GetCountryByIdAsync(int id)
        {
            Country? country =await uow.Countries.GetCountryByIdAsync(id);
            if (country is null) throw new EntityNotExistant("Country wasn't found");
            return mapper.Map<CountryDTO>(country);
        }

        public async Task<CountryDTO?> GetCountryByNameAsync(string name)
        {
            Country? country =await uow.Countries.GetCountryByNameAsync(name);
            if(country is null) throw new EntityNotExistant("Country wasn't found");
            return mapper.Map<CountryDTO>(country);
        }

        public async Task<CountryDTO> UpdateCountryAsync(int id, ManipulateCountryDTO updatedDTO)
        {
            Country? country =await uow.Countries.GetCountryByIdAsync(id);
            if (country is null) throw new EntityNotExistant("Country wasn't found");

            mapper.Map(updatedDTO, country);
            await uow.SaveChangesAsync();
            return mapper.Map<CountryDTO>(country);
        }
    }
}
