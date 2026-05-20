using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IConcertRepository:IRepository<Concert>
    {
        ICollection<Concert> GetConcertsWithDetailsByTour(string tourName);
        ICollection<Concert> GetConcertsWithDetailsByCity(string cityName);
        ICollection<Concert> GetConcertsWithDetailsByCountry(string countryName);
        ICollection<Concert> GetConcertsWithDetails();
        Concert? GetConcertWithDetailsById(int id);
    }
}
