using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public ConcertService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<ConcertDTO> CreateConcertAsync(ManipulateConcertDTO createDTO)
        {
            Tour? tour =await uow.Tours.GetTourWithDetailsByNameAsync(createDTO.TourName);
            if (tour is null) throw new Exceptions.EntityNotExistant("Tour wasn't found");

            City? city =await uow.Cities.GetCityWithDetailsByNameAsync(createDTO.CityName);
            if (city is null) throw new Exceptions.EntityNotExistant("City wasn't found");

            Concert concert = new Concert()
            {
                Price = createDTO.Price,
                Venue = createDTO.Venue,
                StartingTime = createDTO.StartingTime,
                City= city,
                Tour= tour
            };

           await uow.Concerts.InsertAsync(concert);
           await uow.SaveChangesAsync();

            return mapper.Map<ConcertDTO>(concert);
        }

        public async Task DeleteConcertAsync(int id)
        {
            Concert? concert =await uow.Concerts.GetConcertWithDetailsByIdAsync(id);
            if (concert is null) throw new EntityNotExistant("Concert wasn't found");

           await uow.Concerts.DeleteAsync(concert);
           await uow.SaveChangesAsync();
        }

        public async Task<ICollection<ConcertDTO>> GetAllConcertsAsync()
        {
            var concerts =await uow.Concerts.GetConcertsWithDetailsAsync();
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public async Task<ICollection<ConcertDTO>> GetConcertsForCityAsync(string cityName)
        {
            var concerts = await uow.Concerts.GetConcertsWithDetailsByCityAsync(cityName);
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public async Task<ICollection<ConcertDTO>> GetConcertsForCountryAsync(string countryName)
        {
            var concerts = await uow.Concerts.GetConcertsWithDetailsByCountryAsync(countryName);
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public async Task<ICollection<ConcertDTO>> GetConcertsForTourAsync(string tourName)
        {
            var concerts =await uow.Concerts.GetConcertsWithDetailsByTourAsync(tourName);
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public async Task<ConcertDTO> UpdateConcertAsync(int id, ManipulateConcertDTO updateDTO)
        {
            Concert? concert =await uow.Concerts.GetConcertWithDetailsByIdAsync(id);
            if (concert is null) throw new Exceptions.EntityNotExistant("Concert wasn't found");

            City? city =await uow.Cities.GetCityWithDetailsByNameAsync(updateDTO.CityName);
            Tour? tour =await uow.Tours.GetTourWithDetailsByNameAsync(updateDTO.TourName);

            if (city is null) throw new Exceptions.EntityNotExistant("City wasn't found");
            if (tour is null) throw new Exceptions.EntityNotExistant("Tour wasn't found");

            mapper.Map(updateDTO, concert);
           await uow.SaveChangesAsync();
            return mapper.Map<ConcertDTO>(concert);
        } 
    }
}
