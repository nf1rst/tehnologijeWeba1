using HeavyArchives.DAL.Data;
using HeavyArchives.Domain.Interfaces;
using Microsoft.Data.SqlClient.DataClassification;
using HeavyArchives.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
namespace HeavyArchives.DAL.Repositories


{
    public class LabelRepository:BaseRepository<Domain.Entities.Label>, ILabelRepository // ima jos neki interni Label u VS studiju, trebam mijenjat ime ili da samo ovako ostavim
    {
        protected readonly HeavyArchivesContext context;
        public LabelRepository(HeavyArchivesContext context):base(context){
            this.context = context;
        } //tu nema nista jer je u baznoj klasi kontruktor koji speicifira context

        public ICollection<Domain.Entities.Label> GetActiveLabels()
        {
            return context.Labels
                .Where(l => l.Status == "Active")
                .ToList();
        }

        public ICollection<Domain.Entities.Label> GetLabelsByTheme(string themeName)
        {
            return context.Labels
                 .Include(l => l.LabelTheme)
                     .ThenInclude(lt => lt.Theme)
                 .Where(t=>t.LabelTheme.Any(lt=>lt.Theme.Name==themeName))
                 .ToList();
        }

        public ICollection<Domain.Entities.Label> GetAllLabels()
        {
            return context.Labels
                .ToList();
        }

        public ICollection<Domain.Entities.Label> GetLabelsWithDetailsByBand(int bandId)
        {
            return context.Labels
                .Include(l => l.BandLabel)
                    .ThenInclude(bl => bl.Band)
                       .ThenInclude(b => b.Country)
                .Include(l => l.LabelTheme)
                    .ThenInclude(lt => lt.Theme)
                .Where(l=>l.BandLabel.Any(bl=>bl.Band.Id==bandId))
                .ToList();
        }

        public ICollection<Domain.Entities.Label> GetLabelsWithDetailsByBand(string bandName)
        {
            return context.Labels
                .Include(l => l.LabelTheme)
                    .ThenInclude(bt => bt.Theme)
                .Where(l => l.BandLabel.Any(bl => bl.Band.Name == bandName))
                .ToList();
        }

        public Domain.Entities.Label? GetLabelWithDetails(int id)
        {
            return context.Labels
                .Include(l => l.LabelTheme)
                    .ThenInclude(lt => lt.Theme)
                .Include(l => l.BandLabel)
                    .ThenInclude(bl => bl.Band)
                       .ThenInclude(b => b.Country)
                .FirstOrDefault(l => l.Id == id);
        }

        public Domain.Entities.Label? GetLabelWithDetailsByName(string name)
        {
            return context.Labels
                .Include(l => l.LabelTheme)
                    .ThenInclude(lt => lt.Theme)
                .Include(l => l.BandLabel)
                    .ThenInclude(bl => bl.Band)
                       .ThenInclude(b => b.Country)
                .FirstOrDefault(l => l.Name == name);
        }


    }
}
