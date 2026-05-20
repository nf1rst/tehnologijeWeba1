using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities.M2MTables
{
    public class GenreBand
    {
        public int Id{ get; set; }
        public Genre Genre{ get; set; }
        public Band Band{ get; set; }
        public int BandId{ get; set; }
        public int GenreId{ get; set; }
    }
}
