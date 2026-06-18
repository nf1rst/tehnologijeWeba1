using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Interfaces.M2M
{
    public interface IBandLabelRepository
    {
        Task AddBandLabelAsync(BandLabel bandLabel);
        Task RemoveBandLabelAsync(BandLabel bandLabel);
    }
}
