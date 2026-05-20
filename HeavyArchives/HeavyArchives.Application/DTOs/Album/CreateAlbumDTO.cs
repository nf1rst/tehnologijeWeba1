using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.DTOs.Album
{
    public class CreateAlbumDTO
    {
        public string Name { get; set; }
        public int BandId { get; set; }
        public DateOnly DateOfPublishing { get; set; }
        public string Type{ get; set; }
    }
}
