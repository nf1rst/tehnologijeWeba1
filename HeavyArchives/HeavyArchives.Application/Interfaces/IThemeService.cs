using HeavyArchives.Application.DTOs.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface IThemeService
    {
        Task<ICollection<ThemeDTO>> GetAllThemesAsync();
        Task<ThemeDTO?> GetThemeByIdAsync(int id);
        Task<ThemeDTO?> GetThemeByNameAsync(string name);
        Task<ICollection<ThemeDTO>> GetThemesForBandAsync(string bandName);
        Task<ICollection<ThemeDTO>> GetThemesForLabelAsync(string labelName);
        Task<ThemeDTO> CreateThemeAsync(ManipulateThemeDTO createDTO);
        Task DeleteThemeAsync(int id);
        Task<ThemeDTO > UpdateThemeAsync(int id, ManipulateThemeDTO updateDTO);
    }
}
