using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IBandRepository:IRepository<Band>
    {
        Band? GetBandWithDetails(int id); // ukljucuje sve propertyje
        ICollection<Band> GetBandByCountry(string countryName);
        ICollection<Band> GetBandsByGenre(string genreName);
        ICollection<Band> GetActiveBands();
        ICollection<Band> GetBandsByLabel(int labelId);
        ICollection<Band> GetBandsWithDetails();
        Band? GetBandWithDetailsByName(string name);
        ICollection<Band> GetAllBands();
    }
}
