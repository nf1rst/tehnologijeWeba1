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
        Task<ICollection<CountryDTO>> GetAllCountriesAsync();
        Task<CountryDTO?> GetCountryByIdAsync(int id);
        Task<CountryDTO?> GetCountryByNameAsync(string name);
        Task<CountryDTO> CreateCountryAsync(ManipulateCountryDTO createDTO);
        Task<CountryDTO >UpdateCountryAsync(int id, ManipulateCountryDTO updatedDTO);
        Task DeleteCountryAsync(int id);
    }
}
