using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.Domain.Entities
{
    public class Log
    {
        public int Id{ get; set; }
        public string IpAdress{ get; set; }
        public DateTime TimeStamp{ get; set; }
    }
}
