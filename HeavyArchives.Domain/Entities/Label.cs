using HeavyArchives.Domain.Entities.M2MTables;
namespace HeavyArchives.Domain.Entities
{
    public class Label
    {
        public int Id{ get; set; }
        public string Name { get; set; } = String.Empty;
        public string Adress { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;//enum??
        public DateOnly FoundingDate{ get; set; }
        public ICollection<LabelTheme> LabelTheme { get; set; } = new List<LabelTheme>();
        public ICollection<BandLabel> BandLabel { get; set; }=new List<BandLabel>();
    }
}
