using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Interfaces;
using Microsoft.Data.SqlClient.DataClassification;
using HeavyArchives.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace HeavyArchives.DAL.Repositories


{
    public class LabelRepository:BaseRepository<Domain.Entities.Label>, ILabelRepository 
    {
        protected readonly HeavyArchivesContext context;
        public LabelRepository(HeavyArchivesContext context):base(context){
            this.context = context;
        } 


        public async Task<ICollection<Domain.Entities.Label>> GetLabelsByThemeAsync(string theme)
        {
            return await context.Labels
                 .Include(l => l.LabelTheme)
                     .ThenInclude(lt => lt.Theme)
                 .Where(t => t.LabelTheme.Any(lt => lt.Theme.Name == theme))
                 .ToListAsync();
        }

        public async Task<ICollection<Domain.Entities.Label>> GetAllLabelsAsync()
        {
            return await context.Labels
                .ToListAsync();
        }

        public async Task<ICollection<Domain.Entities.Label>> GetLabelsWithDetailsByBandAsync(string bandName)
        {
            return await  context.Labels
                .Include(l => l.BandLabel)
                    .ThenInclude(bl => bl.Band)
                       .ThenInclude(b => b.Country)
                .Include(l => l.LabelTheme)
                    .ThenInclude(lt => lt.Theme)
                .Where(l => l.BandLabel.Any(bl => bl.Band.Name == bandName))
                .ToListAsync();
        }

        public async Task<Domain.Entities.Label?> GetLabelWithDetailsAsync(int id)
        {
            return await context.Labels
                .Include(l => l.LabelTheme)
                    .ThenInclude(lt => lt.Theme)
                .Include(l => l.BandLabel)
                    .ThenInclude(bl => bl.Band)
                       .ThenInclude(b => b.Country)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Domain.Entities.Label?> GetLabelWithDetailsByNameAsync(string name)
        {
            return await  context.Labels
                  .Include(l => l.LabelTheme)
                      .ThenInclude(lt => lt.Theme)
                  .Include(l => l.BandLabel)
                      .ThenInclude(bl => bl.Band)
                         .ThenInclude(b => b.Country)
                  .FirstOrDefaultAsync(l => l.Name == name);
        }

        public  async Task<ICollection<Domain.Entities.Label>> GetActiveLabelsAsync()
        {
            return await context.Labels
                .Where(l => l.Status == "Active")
                .ToListAsync();
        }
    }
}
