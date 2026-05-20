namespace HeavyArchives.Application.Interfaces
{
    public interface IPasswordHasher
    {
        (string hash, string salt) HashPassword(string password); // sto znaci ovaj zapis ispred
        bool VerifyPassword(string password, string hash, string salt);   
    }
}
