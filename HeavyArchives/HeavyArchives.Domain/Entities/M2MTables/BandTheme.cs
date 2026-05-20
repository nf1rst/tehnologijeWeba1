using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities.M2MTables
{
    public class BandTheme
    {
        public int Id{ get; set; }
        public Band Band{ get; set; }
        public Theme Theme{ get; set; }
        public int ThemeId{ get; set; }
        public int BandId{ get; set; }
    }
}
