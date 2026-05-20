namespace HeavyArchives.Application.DTOs.Album
{
    public class AlbumDTO
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string BandName { get; set; }
        public DateOnly DateOfPublishing { get; set; }
        public string Type{ get; set; }
    }
}
