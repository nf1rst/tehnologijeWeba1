using HeavyArchives.Application.DTOs.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface ITourService
    {
        TourDTO? GetTourById(int id);
        TourDTO? GetTourByName(string tourName);
        ICollection<TourDTO> GetAllTours();
        ICollection<TourDTO> GetAllToursByManager(string manager);
        TourDTO AddSupportBand(int tourId, int bandId);
        TourDTO RemoveSupportBand(int tourId, int bandId);
        TourDTO AddConcert(int tourId, int concertId);
        TourDTO RemoveConcert(int tourId, int concertId);
        void Delete(int id);
        TourDTO CreateTour(ManipulateTourDTO createDTO);
        TourDTO UpdateTour(int id, ManipulateTourDTO updateDTO);

    }
}
