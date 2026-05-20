using HeavyArchives.Domain.Entities.M2MTables;
using System.Reflection.Metadata.Ecma335;

namespace HeavyArchives.Domain.Entities
{
    public class BandMemberStatus // ovo tu je mogo bit enum, ali na ovaj nacin adin moze dodavat nove vrijednosti 
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public ICollection<BandPerson> BandPerson{ get; set; }
    }
}
