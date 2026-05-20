using HeavyArchives.Domain.Entities.M2MTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Genre
    {
        public int Id{ get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
        public ICollection<GenreBand> GenreBand { get; set; } = new List<GenreBand>();
    }
}
