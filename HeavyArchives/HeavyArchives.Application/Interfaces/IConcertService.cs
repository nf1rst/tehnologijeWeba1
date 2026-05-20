using HeavyArchives.Application.DTOs.Concert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface IConcertService
    {
        ICollection<ConcertDTO> GetConcertsForTour(string tourName);
        ICollection<ConcertDTO> GetConcertsForCity(string cityName);
        ICollection<ConcertDTO> GetConcertsForCountry(string countryName);
        ICollection<ConcertDTO> GetAllConcerts();
        ConcertDTO CreateConcert(ManipulateConcertDTO createDTO);
        ConcertDTO  UpdateConcert(int id, ManipulateConcertDTO updateDTO);
        void DeleteConcert(int id);

    }
}
