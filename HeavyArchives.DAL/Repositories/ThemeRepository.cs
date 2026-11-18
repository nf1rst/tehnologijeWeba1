using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeavyArchives.DAL.Repositories
{
    public class ThemeRepository : BaseRepository<Theme>, IThemeRepository
    {
        private readonly HeavyArchivesContext context;
        public ThemeRepository(HeavyArchivesContext context):base(context)
        {
            this.context = context;
        }
  

        public async Task<ICollection<Theme>> GetThemesWithDetailsAsync()
        {
            return await context.Themes
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .ToListAsync();
        }

        public async Task<ICollection<Theme>> GetThemesWithDetailsForBandAsync(int bandId)
        {
            return await context.Themes
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Where(t => t.BandTheme.Any(bt => bt.Band.Id == bandId))
                .ToListAsync();
        }


        public async Task<ICollection<Theme>> GetThemesWithDetailsForLabelAsync(int labelId)
        {
            return await context.Themes
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Where(t => t.LabelTheme.Any(lt => lt.Label.Id == labelId))
                .ToListAsync();
        }

        public async Task<Theme?> GetThemeWithDetailsByIdAsync(int id)
        {
            return await context.Themes
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Theme?> GetThemeWithDetailsByNameAsync(string name)
        {
            return await context.Themes
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .FirstOrDefaultAsync(b => b.Name == name);
        }
    }
}
