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
        ICollection<Theme> GetThemesWithDetails();

        Theme GetThemeWithDetailsById(int id);
        Theme GetThemeWithDetailsByName(string name);
        ICollection<Theme> GetThemesWithDetailsForLabel(int labelId);
        ICollection<Theme> GetThemesWithDetailsForBand(int bandId);
    }
}
