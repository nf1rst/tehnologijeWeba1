using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Tour;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces;

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
        public async Task<TourDTO> AddConcertAsync(int tourId, int concertId)
        {
            Tour? tour =await uow.Tours.GetTourWithDetailsByIdAsync(tourId)!;
            if (tour is null) throw new EntityNotExistant("There is no such tour");

            Concert? concert =await uow.Concerts.GetConcertWithDetailsByIdAsync(concertId)!;
            if (concert is null) throw new EntityNotExistant("There is no such concert");

            if (tour.Concerts.Contains(concert)) throw new RelationExistsException("Concert already part of the tour");

            var attributedConcerts = await uow.Concerts.GetConcertsWithDetailsByTourAsync(tour.Name);

            foreach (var item in attributedConcerts)
            {
                if (( item.StartingTime == concert.StartingTime))
                {
                    throw new InvalidLogicException("Tour cannot contain concerts that are happening at the same time ");
                }
            }

            tour.Concerts.Add(concert);
            tour.Revenue += concert.Revenue;
            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour); // jel ga tu moram jos jednom izvuc iz baze ili ce se odmah vidjet promijene 
        }

        public async Task<TourDTO> AddSupportBandAsync(int tourId, int bandId)
        {
            Tour? tour = await uow.Tours.GetTourWithDetailsByIdAsync(tourId)!;
            if (tour is null) throw new EntityNotExistant("There is no such tour");

            Band? band = await uow.Bands.GetBandWithDetailsAsync(bandId)!;
            if (band is null) throw new EntityNotExistant("There is no such band");


            tour.TourBand.Add(new BandTour() { BandId = bandId, TourID = tourId, IsHeadliner = false });
            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour);
        }

        public async Task<TourDTO> CreateTourAsync(ManipulateTourDTO createDTO)
        {
            Person? person =await uow.Persons.GetByIdAsync(createDTO.TourManagerID);
            if (person is null) throw new EntityNotExistant("Person cannot manage tour as he doesn't exist");
            Band? healiderBand = await uow.Bands.GetBandWithDetailsByNameAsync(createDTO.HeadlinerBand);
            if (healiderBand is null) throw new EntityNotExistant("Headliner band does not exist");


            Tour tour = mapper.Map<Tour>(createDTO);
            tour.TourManager = person;

            if (tour.StartingDate > tour.FinishingDate)
            {
                throw new InvalidLogicException("Tour cannot finish before it started");
            }

            if (healiderBand.Status != "Active")
            {
                throw new InvalidLogicException("Headlinder band must be active to take part in tour");
            }

            await uow.Tours.InsertAsync(tour);
            tour.TourBand.Add(new BandTour() { Band = healiderBand, Tour = tour, IsHeadliner = true });

            
            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour);
        }

        public async Task DeleteAsync(int id)
        {
            Tour? tour =await uow.Tours.GetByIdAsync(id);
            if (tour is null) throw new EntityNotExistant("There is not such band");

            await uow.Tours.DeleteAsync(tour);
            await  uow.SaveChangesAsync();
        }

        public async Task<ICollection<TourDTO>> GetAllToursAsync()
        {
            var tours =await uow.Tours.GetAllToursWithDetailsAsync();
            return mapper.Map<ICollection<TourDTO>>(tours);
        }

        public async Task<ICollection<TourDTO>> GetAllToursByManagerAsync(string manager)
        {
            string[] info = manager.Split(' ');
            if (info.Length != 2) throw new Exceptions.InvalidInformationException("You must enter first and second name");

            Person? person =await uow.Persons.GetPersonByFirstAndLastNameAsync(info[0], info[1]);
            if (person is null) throw new EntityNotExistant("That person does not exist");
            ICollection<Tour> tours =await uow.Tours.GetAllToursWithDetailsByManagerAsync(person.Id);
            return mapper.Map<ICollection<TourDTO>>(tours);
        }

        public async Task<TourDTO?> GetTourByIdAsync(int id)
        {
            Tour? tour =await uow.Tours.GetTourWithDetailsByIdAsync(id);
            if (tour is null) throw new EntityNotExistant("That tour does not exist");
            return mapper.Map<TourDTO>(tour);
        }

        public async Task<TourDTO?> GetTourByNameAsync(string tourName)
        {
            Tour? tour =await uow.Tours.GetTourWithDetailsByNameAsync(tourName);
            if (tour is null) throw new EntityNotExistant("That tour does not exist");
            return mapper.Map<TourDTO>(tour);

        }

        public async Task<TourDTO> RemoveConcertAsync(int tourId, int concertId)
        {
            Tour? tour = await uow.Tours.GetTourWithDetailsByIdAsync(tourId)!;
            if (tour is null) throw new EntityNotExistant("There is no such tour");

            Concert? concert = await uow.Concerts.GetConcertWithDetailsByIdAsync(concertId)!;
            if (concert is null) throw new EntityNotExistant("There is no such concert");

            tour.Concerts.Remove(concert);
            tour.Revenue -= concert.Revenue;
            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour);
        }

        public async Task<TourDTO> RemoveSupportBandAsync(int tourId, int bandId)
        {
            Tour? tour = await uow.Tours.GetTourWithDetailsByIdAsync(tourId)!;
            if (tour is null) throw new EntityNotExistant("There is no such tour");

            //Band? band = await uow.Bands.GetBandWithDetailsAsync(bandId);
            //if (band is null) throw new EntityNotExistant("There is not such band");

            BandTour? entry = tour.TourBand.FirstOrDefault(tb => tb.BandId == bandId);
            if (entry is null) throw new EntityNotExistant("Band not associated with tour");


            //await uow.BandTours.RemoveBandTourAsync(entry);
            tour.TourBand.Remove(entry);

            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour);
        }

        public async Task<TourDTO> UpdateTourAsync(int id, ManipulateTourDTO updateDTO)
        {
            Tour? tour =await uow.Tours.GetTourWithDetailsByIdAsync(id);
            if (tour is null) throw new EntityNotExistant("There is no such tour");

            mapper.Map(updateDTO, tour);
            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour);
        }
    }
}
