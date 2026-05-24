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
        public async Task<TourDTO> AddConcertAsync(int tourId, int concertId)
        {
            Tour? tour =await uow.Tours.GetTourWithDetailsByIdAsync(tourId)!;
            Concert? concert =await uow.Concerts.GetConcertWithDetailsByIdAsync(concertId)!;

            if (tour is null) throw new EntityNotExistant("There is no such tour");
            if (concert is null) throw new EntityNotExistant("There is no such concert");

            tour.Concerts.Add(concert);
            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour); // jel ga tu moram jos jednom izvuc iz baze ili ce se odmah vidjet promijene 
        }

        public async Task<TourDTO> AddSupportBandAsync(int tourId, int bandId)
        {
            Tour? tour = await uow.Tours.GetTourWithDetailsByIdAsync(tourId)!;
            Band? band = await uow.Bands.GetBandWithDetailsAsync(bandId)!;

            if (tour is null) throw new EntityNotExistant("There is no such tour");
            if (band is null) throw new EntityNotExistant("There is no such band");

            tour.SupportBands.Add(band);
            await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour);
        }

        public async Task<TourDTO> CreateTourAsync(ManipulateTourDTO createDTO)
        {
            Tour tour = mapper.Map<Tour>(createDTO);

            await uow.Tours.InsertAsync(tour);
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
            var tours = uow.Tours.GetAllToursWithDetailsByManagerAsync(person.Id);
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
            Concert? concert = await uow.Concerts.GetConcertWithDetailsByIdAsync(concertId)!;

            if (tour is null) throw new EntityNotExistant("There is no such tour");
            if (concert is null) throw new EntityNotExistant("There is no such concert");

            tour.Concerts.Remove(concert);
           await uow.SaveChangesAsync();
            return mapper.Map<TourDTO>(tour);
        }

        public async Task<TourDTO> RemoveSupportBandAsync(int tourId, int bandId)
        {
            Tour? tour = await uow.Tours.GetTourWithDetailsByIdAsync(tourId)!;
            Band? band = await uow.Bands.GetBandWithDetailsAsync(bandId)!;

            if (tour is null) throw new EntityNotExistant("There is no such tour");
            if (band is null) throw new EntityNotExistant("There is no such band");

            tour.SupportBands.Remove(band);
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
