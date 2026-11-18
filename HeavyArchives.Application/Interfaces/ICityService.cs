using EncyclopaediaBisemutum.Application.DTOs.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Application.Interfaces
{
    public interface ICityService
    {
        Task<ICollection<CityDTO>> GetCitiesByCountry(string countryName);
        Task<CityWithDetailsDTO?> CreateCity(ManipulateCityDTO createDTO);
        Task<CityWithDetailsDTO?> GetCityByName(string name);
        Task<CityWithDetailsDTO?> GetCityById(int id);
        Task<CityWithDetailsDTO?> UpdateCity(int id, ManipulateCityDTO updateDTO);
        Task<ICollection<CityDTO>> GetAllCities();
        Task DeleteCity(int id);
    }
}
