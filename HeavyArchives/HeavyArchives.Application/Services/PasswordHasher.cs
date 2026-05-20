using HeavyArchives.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
        public (string hash, string salt) HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize); // ovo je da se osigura da ista lozinka ne da isti hash 
            //rainbow table naprad preventa 

            var hash = Rfc2898DeriveBytes.Pbkdf2( //pseudo random number generator koji koristi pbkdf2 algoritam
                password,
                salt,
                Iterations, // ovo da uspori brute force napad
                Algorithm,
                HashSize);
            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hashBytes = Convert.FromBase64String(hash);

            var computedHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                Iterations,
                Algorithm,
                HashSize);
            //ponovno iracuna hash i usporedi 
            return CryptographicOperations.FixedTimeEquals(computedHash, hashBytes); // FixedTimeEquals - sprjecava timing attack
        }
    }
}
