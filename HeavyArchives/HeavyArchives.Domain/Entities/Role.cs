namespace HeavyArchives.Domain.Entities
{
    public class Role
    {
        public int Id{ get; set; }
        public string Name { get; set; } = String.Empty;
        public ICollection<User> Users{ get; set; } = new List<User>();

        // u contextu je namjesteno da se u bazu stave 2 usera, Admin(1) i User(2)
    }
}
