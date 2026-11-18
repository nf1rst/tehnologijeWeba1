using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Album
    {
        public int Id{ get; set; }
        public string Name { get; set; } = String.Empty;
        public int BandId{ get; set; }
        public Band Band { get; set; } = null;
        public DateOnly DateOfPublishing{ get; set; }
        public string Type { get; set; } = String.Empty; 
    }
}
