namespace HeavyArchives.Application.DTOs.Auth
{
    public class UserDTO
    {
        public int Id{ get; set; }
        public string Username{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public string Role{ get; set; }
        public string FavoriteBand { get; set; }
    }
}
