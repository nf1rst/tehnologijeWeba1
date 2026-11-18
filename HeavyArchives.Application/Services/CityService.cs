using AutoMapper;
using EncyclopaediaBisemutum.Application.DTOs.City;
using EncyclopaediaBisemutum.Application.Interfaces;
using Exceptions;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CityService(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper= mapper;
            this.uow = uow;
        }
        public async Task<CityWithDetailsDTO?> CreateCity(ManipulateCityDTO createDTO)
        {
            Country? country = await uow.Countries.GetCountryByNameAsync(createDTO.CountryName);
            if (country is null) throw new EntityNotExistant($"Country [{createDTO.CountryName}] was not found");

            City city = new City()
            {
                Name= createDTO.Name,
                Country= country
            };
            await uow.Cities.InsertAsync(city);
            await uow.SaveChangesAsync();
            return mapper.Map<CityWithDetailsDTO>(city);
        }

        public async Task DeleteCity(int id)
        {
            var city = await uow.Cities.GetByIdAsync(id);
            if (city is null) throw new EntityNotExistant("City does not exist");

            await uow.Cities.DeleteAsync(city);
            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<CityDTO>> GetAllCities()
        {
            var cities = await uow.Cities.GetAllCitiesWithDetails();
            return mapper.Map<ICollection<CityDTO>>(cities);
        }

        public async Task<ICollection<CityDTO>> GetCitiesByCountry(string countryName)
        {
            var cities = await uow.Cities.GetCitiesWithDetailsByCountryAsync(countryName);
            return mapper.Map<ICollection<CityDTO>>(cities);
        }

        public async Task<CityWithDetailsDTO?> GetCityById(int id)
        {
            City? city = await uow.Cities.GetCityWithDetailsByIdAsync(id);
            if (city is null) throw new EntityNotExistant("City was not found");
            return mapper.Map<CityWithDetailsDTO>(city);
        }

        public async Task<CityWithDetailsDTO?> GetCityByName(string name)
        {
            City? city = await uow.Cities.GetCityWithDetailsByNameAsync(name);
            if (city is null) throw new EntityNotExistant("City was not found");
            return mapper.Map<CityWithDetailsDTO>(city);
        }

        public async Task<CityWithDetailsDTO?> UpdateCity(int id, ManipulateCityDTO updateDTO)
        {
            City? city = await uow.Cities.GetCityWithDetailsByIdAsync(id);
            if (city is null) throw new EntityNotExistant("City was not found");

            Country? country = await uow.Countries.GetCountryByNameAsync(updateDTO.CountryName);
            if (country is null) throw new EntityNotExistant("Country was not found");

            city.Name= updateDTO.Name;
            city.Country = country;

            await uow.Cities.UpdateAsync(city);
            await uow.SaveChangesAsync();
            return mapper.Map<CityWithDetailsDTO>(city);
        }
    }
}
