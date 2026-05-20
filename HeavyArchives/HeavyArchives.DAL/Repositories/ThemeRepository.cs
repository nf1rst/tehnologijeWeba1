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
        public ICollection<Theme> GetThemesWithDetails()
        {
            return context.Themes
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .ToList();
        }

        public ICollection<Theme> GetThemesWithDetailsForBand(int bandId)
        {
            return context.Themes
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Where(t => t.BandTheme.Any(bt => bt.Band.Id == bandId))
                .ToList();
        }

        public ICollection<Theme> GetThemesWithDetailsForLabel(int labelId)
        {
            return context.Themes
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Where(t => t.LabelTheme.Any(lt => lt.Label.Id==labelId))
                .ToList();
        }

        public Theme GetThemeWithDetailsById(int id)
        {
            return context.Themes
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .FirstOrDefault(b => b.Id == id);
        }

        public Theme GetThemeWithDetailsByName(string name)
        {
            return context.Themes
                .Include(t => t.BandTheme)
                    .ThenInclude(bt => bt.Band)
                .Include(t => t.LabelTheme)
                    .ThenInclude(lt => lt.Label)
                .FirstOrDefault(b => b.Name == name);
        }
    }
}
