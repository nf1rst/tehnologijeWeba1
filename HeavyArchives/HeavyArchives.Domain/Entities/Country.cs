using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Domain.Entities
{
    public class Country
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public ICollection<Band> Bands { get; set; } = new List<Band>();//jel ovo opce potrebno tu 
        public ICollection<Person> Persons { get; set; } = new List<Person>(); // i ovo isto
        public ICollection<City> Cities{ get; set; }=new List<City>();
    }
}
