using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface IThemeRepository:IRepository<Theme>
    {
        Task<ICollection<Theme>> GetThemesWithDetailsAsync();
        Task<Theme?> GetThemeWithDetailsByIdAsync(int id);
        Task<Theme?> GetThemeWithDetailsByNameAsync(string name);
        Task<ICollection<Theme>> GetThemesWithDetailsForLabelAsync(int labelId);
        Task<ICollection<Theme>> GetThemesWithDetailsForBandAsync(int bandId);
    }
}
