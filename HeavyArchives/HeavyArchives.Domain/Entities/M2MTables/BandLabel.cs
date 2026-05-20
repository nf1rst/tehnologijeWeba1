using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities.M2MTables
{
    public class BandLabel
    {
        public int Id{ get; set; }
        public int BandId { get; set; }
        public int LabelId { get; set; }
        public Band Band{ get; set; } //navigational
        public Label Label{ get; set; } //navigational
    }
}
