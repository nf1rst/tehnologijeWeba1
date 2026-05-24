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
        Task<ICollection<Concert>> GetConcertsWithDetailsByTourAsync(string tourName);
        Task<ICollection<Concert>> GetConcertsWithDetailsByCityAsync(string cityName);
        Task<ICollection<Concert>> GetConcertsWithDetailsByCountryAsync(string countryName);
        Task<ICollection<Concert>> GetConcertsWithDetailsAsync();
        Task<Concert? >GetConcertWithDetailsByIdAsync(int id);
    }
}
