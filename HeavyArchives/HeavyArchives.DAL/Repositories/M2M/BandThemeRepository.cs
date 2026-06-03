using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces.M2M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories.M2M
{
    public class BandThemeRepository:IBandThemeRepository
    {
        private readonly HeavyArchivesContext context;

        public BandThemeRepository(HeavyArchivesContext context)
        {
            this.context = context;
        }

        public async Task AddBandThemeAsync(BandTheme bandTheme)
        {
            await context.BandThemes.AddAsync(bandTheme);
        }
    }
}
