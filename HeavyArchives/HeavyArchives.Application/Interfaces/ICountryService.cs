using HeavyArchives.Application.DTOs.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface ICountryService
    {
        ICollection<CountryDTO> GetAllCountries();
        CountryDTO? GetCountryById(int id);
        CountryDTO? GetCountryByName(string name);
        CountryDTO CreateCountry(ManipulateCountryDTO createDTO);
        CountryDTO UpdateCountry(int id, ManipulateCountryDTO updatedDTO);
        void DeleteCountry(int id);
    }
}
