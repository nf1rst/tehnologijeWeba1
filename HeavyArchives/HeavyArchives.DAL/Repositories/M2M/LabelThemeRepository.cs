using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities.M2MTables;
using HeavyArchives.Domain.Interfaces;
using HeavyArchives.Domain.Interfaces.M2M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Repositories.M2M
{
    public class LabelThemeRepository : ILabelThemeRepository
    {
        private readonly HeavyArchivesContext context;
        public LabelThemeRepository(HeavyArchivesContext context)
        {
            this.context = context;
        }
        public async Task AddLabelThemeAsync(LabelTheme labelTheme)
        {
            await context.LabelTheme.AddAsync(labelTheme);
        }

        public Task RemoveLabelThemeAsync(LabelTheme labelTheme)
        {
            context.Remove(labelTheme);
            return Task.CompletedTask;
        }
    }
}
