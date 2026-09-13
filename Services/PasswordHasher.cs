using System.Security.Cryptography;

namespace CommunitySportsBooking.Services
{
    /// <summary>
    /// Simple, dependency-free password hashing using PBKDF2 (Rfc2898DeriveBytes),
    /// which ships in the base class library. Salt is unique per member and stored
    /// alongside the hash so passwords are never kept in plain text.
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 16;   // 128 bit
        private const int KeySize = 32;    // 256 bit
        private const int Iterations = 100_000;

        public static (string Hash, string Salt) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
            return (Convert.ToBase64String(key), Convert.ToBase64String(salt));
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
            return CryptographicOperations.FixedTimeEquals(key, Convert.FromBase64String(storedHash));
        }
    }
}