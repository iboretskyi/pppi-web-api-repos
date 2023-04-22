using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AnimeWebAPI.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashedPassword}";
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var parts = passwordHash.Split('.', 2);
            if (parts.Length != 2)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword == parts[1];
        }
    }
}
