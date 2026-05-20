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
        ICollection<ThemeDTO> GetAllThemes();
        ThemeDTO? GetThemeById(int id);
        ThemeDTO? GetThemeByName(string name);
        ICollection<ThemeDTO> GetThemesForBand(string bandName);
        ICollection<ThemeDTO> GetThemesForLabel(string labelName);
        ThemeDTO CreateTheme(ManipulateThemeDTO createDTO);
        void DeleteTheme(int id);
        ThemeDTO UpdateTheme(int id, ManipulateThemeDTO updateDTO);
    }
}
