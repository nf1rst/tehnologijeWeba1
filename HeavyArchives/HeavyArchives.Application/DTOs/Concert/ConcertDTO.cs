using HeavyArchives.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Concert
{
    public class ConcertDTO
    {
        public int Id { get; set; }
        public string PartOfTour{ get; set; }
        public double Price { get; set; }
        public string City{ get; set; }
        public DateTime StartingTime { get; set; }
        public string Venue { get; set; }
    }
}
