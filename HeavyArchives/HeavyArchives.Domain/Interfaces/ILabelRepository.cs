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
        ICollection<Label> GetLabelsByTheme(string theme);
        ICollection<Label> GetAllLabels();
        ICollection<Label> GetLabelsWithDetailsByBand(string bandName);
        Label GetLabelWithDetails(int id);
        Label GetLabelWithDetailsByName(string name);
        ICollection<Label> GetActiveLabels();

    }
}
