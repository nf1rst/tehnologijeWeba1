using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities.M2MTables
{
    public class LabelTheme
    {
        public int Id{ get; set; }
        public int LabelId{ get; set; }
        public int ThemeId{ get; set; }
        public Label Label{ get; set; }
        public Theme Theme{ get; set; }

    }
}
