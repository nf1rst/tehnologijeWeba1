using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Concert;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;

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
        public ConcertDTO CreateConcert(ManipulateConcertDTO createDTO)
        {
            Tour tour = uow.Tours.GetTourWithDetailsByName(createDTO.TourName);
            if (tour is null) throw new Exceptions.EntityNotExistant("Tour wasn't found");

            City city = uow.Cities.GetCityWithDetailsByName(createDTO.CityName);
            if (city is null) throw new Exceptions.EntityNotExistant("City wasn't found");

            Concert concert = new Concert()
            {
                Price = createDTO.Price,
                Venue = createDTO.Venue,
                StartingTime = createDTO.StartingTime,
                City= city,
                Tour= tour
            };

            uow.Concerts.Insert(concert);
            uow.SaveChanges();

            return mapper.Map<ConcertDTO>(concert);
        }

        public void DeleteConcert(int id)
        {
            Concert? concert = uow.Concerts.GetConcertWithDetailsById(id);
            if (concert is null) throw new EntityNotExistant("Concert wasn't found");

            uow.Concerts.Delete(concert);
            uow.SaveChanges();
        }

        public ICollection<ConcertDTO> GetAllConcerts()
        {
            var concerts = uow.Concerts.GetConcertsWithDetails();
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public ICollection<ConcertDTO> GetConcertsForCity(string cityName)
        {
            var concerts = uow.Concerts.GetConcertsWithDetailsByCity(cityName);
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public ICollection<ConcertDTO> GetConcertsForCountry(string countryName)
        {
            var concerts = uow.Concerts.GetConcertsWithDetailsByCountry(countryName);
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public ICollection<ConcertDTO> GetConcertsForTour(string tourName)
        {
            var concerts = uow.Concerts.GetConcertsWithDetailsByTour(tourName);
            return mapper.Map<ICollection<ConcertDTO>>(concerts);
        }

        public ConcertDTO UpdateConcert(int id, ManipulateConcertDTO updateDTO)
        {
            Concert? concert = uow.Concerts.GetConcertWithDetailsById(id);
            if (concert is null) throw new Exceptions.EntityNotExistant("Concert wasn't found");

            City? city = uow.Cities.GetCityWithDetailsByName(updateDTO.CityName);
            Tour? tour = uow.Tours.GetTourWithDetailsByName(updateDTO.TourName);

            if (city is null) throw new Exceptions.EntityNotExistant("City wasn't found");
            if (tour is null) throw new Exceptions.EntityNotExistant("Tour wasn't found");

            mapper.Map(updateDTO, concert);
            uow.SaveChanges();
            return mapper.Map<ConcertDTO>(concert);
        } 
    }
}
