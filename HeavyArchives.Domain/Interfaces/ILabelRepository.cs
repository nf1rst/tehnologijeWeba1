using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces
{
    public interface ILabelRepository:IRepository<Label>
    {
        Task<ICollection<Label>> GetLabelsByThemeAsync(string theme);
        Task<ICollection<Label>> GetAllLabelsAsync();
        Task<ICollection<Label>> GetLabelsWithDetailsByBandAsync(string bandName);
        Task<Label?> GetLabelWithDetailsAsync(int id);
        Task<Label?> GetLabelWithDetailsByNameAsync(string name);
        Task<ICollection<Label>> GetActiveLabelsAsync();

    }
}
