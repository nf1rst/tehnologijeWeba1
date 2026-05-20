using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.DTOs.Tour;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System .Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class TourService : ITourService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TourService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public TourDTO AddConcert(int tourId, int concertId)
        {
            if (!uow.Tours.Exists(tourId)) throw new EntityNotExistant("There is no such tour");
            if (!uow.Concerts.Exists(concertId)) throw new EntityNotExistant("There is no such concert");

            Tour tour = uow.Tours.GetTourWithDetailsById(tourId)!;
            Concert concert = uow.Concerts.GetConcertWithDetailsById(concertId)!;

            tour.Concerts.Add(concert);
            uow.SaveChanges();
            return mapper.Map<TourDTO>(tour); // jel ga tu moram jos jednom izvuc iz baze ili ce se odmah vidjet promijene 
        }

        public TourDTO AddSupportBand(int tourId, int bandId)
        {
            if (!uow.Tours.Exists(tourId)) throw new EntityNotExistant("There is no such tour");
            if (!uow.Bands.Exists(bandId)) throw new EntityNotExistant("There is no such band");

            Tour tour = uow.Tours.GetTourWithDetailsById(tourId)!;
            Band band = uow.Bands.GetBandWithDetails(bandId)!;

            tour.SupportBands.Add(band);
            uow.SaveChanges();
            return mapper.Map<TourDTO>(tour);
        }

        public TourDTO CreateTour(ManipulateTourDTO createDTO)
        {
            Tour tour = mapper.Map<Tour>(createDTO);

            uow.Tours.Insert(tour);
            uow.SaveChanges();
            return mapper.Map<TourDTO>(tour);
        }

        public void Delete(int id)
        {
            if (!uow.Tours.Exists(id)) throw new EntityNotExistant("There is not such band");
            Tour tour = uow.Tours.GetById(id)!;
            uow.Tours.Delete(tour);
            uow.SaveChanges();
        }

        public ICollection<TourDTO> GetAllTours()
        {
            var tours = uow.Tours.GetAllToursWithDetails();
            return mapper.Map<ICollection<TourDTO>>(tours);
        }

        public ICollection<TourDTO> GetAllToursByManager(string manager)
        {
            string[] info = manager.Split(' ');
            if (info.Length != 2) throw new Exceptions.InvalidInformationException("You must enter first and second name");

            Person? person = uow.Persons.GetPersonByFirstAndLastName(info[0], info[1]);
            if (person is null) throw new EntityNotExistant("That person does not exist");
            var tours = uow.Tours.GetAllToursWithDetailsByManager(person.Id);
            return mapper.Map<ICollection<TourDTO>>(tours);
        }

        public TourDTO? GetTourById(int id)
        {
            Tour? tour = uow.Tours.GetTourWithDetailsById(id);
            if (tour is null) throw new EntityNotExistant("That tour does not exist");
            return mapper.Map<TourDTO>(tour);
        }

        public TourDTO? GetTourByName(string tourName)
        {
            Tour? tour = uow.Tours.GetTourWithDetailsByName(tourName);
            if (tour is null) throw new EntityNotExistant("That tour does not exist");
            return mapper.Map<TourDTO>(tour);

        }

        public TourDTO RemoveConcert(int tourId, int concertId)
        {
            if (!uow.Tours.Exists(tourId)) throw new EntityNotExistant("There is no such tour");
            if (!uow.Bands.Exists(concertId)) throw new EntityNotExistant("There is no such concert");

            Tour tour = uow.Tours.GetTourWithDetailsById(tourId)!;
            Concert concert = uow.Concerts.GetConcertWithDetailsById(concertId)!;

            tour.Concerts.Remove(concert);
            uow.SaveChanges();
            return mapper.Map<TourDTO>(tour);
        }

        public TourDTO RemoveSupportBand(int tourId, int bandId)
        {
            if (!uow.Tours.Exists(tourId)) throw new EntityNotExistant("There is no such tour");
            if (!uow.Bands.Exists(bandId)) throw new EntityNotExistant("There is no such band");

            Tour tour = uow.Tours.GetTourWithDetailsById(tourId)!;
            Band band = uow.Bands.GetBandWithDetails(bandId)!;

            tour.SupportBands.Remove(band);
            uow.SaveChanges();
            return mapper.Map<TourDTO>(tour);
        }

        public TourDTO UpdateTour(int id, ManipulateTourDTO updateDTO)
        {
            Tour? tour = uow.Tours.GetTourWithDetailsById(id);
            if (tour is null) throw new EntityNotExistant("There is no such tour");

            mapper.Map(updateDTO, tour);
            uow.SaveChanges();
            return mapper.Map<TourDTO>(tour);
        }
    }
}
