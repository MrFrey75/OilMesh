using System.Security.Cryptography;
using OilCore.Models;

namespace OilCore.Services
{
    /// <summary>
    /// Provides secure password hashing and verification using PBKDF2 with SHA-512.
    /// </summary>
    public static class EncryptionService
    {
        private const int SaltSize = 32; // 256-bit salt
        private const int HashSize = 64; // 512-bit hash
        private const int Iterations = 100000; // Recommended iterations for security

        /// <summary>
        /// Generates a hashed password and corresponding salt.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <returns>A tuple containing the password hash and salt.</returns>
        public static (string hash, string salt) HashPassword(string password)
        {
            ValidatePasswordComplexity(password);

            var saltBytes = new byte[SaltSize];
            RandomNumberGenerator.Fill(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var hash = ComputeHash(password, saltBytes);
            return (hash, salt);
        }

        /// <summary>
        /// Verifies if the provided password matches the stored hash and salt.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <param name="storedHash">The stored password hash.</param>
        /// <param name="storedSalt">The stored salt.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
                return false;

            var saltBytes = Convert.FromBase64String(storedSalt);
            var computedHash = ComputeHash(password, saltBytes);

            // Use a constant-time comparison to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(computedHash), Convert.FromBase64String(storedHash));
        }

        /// <summary>
        /// Computes the hash of a password using PBKDF2 with SHA-512.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>The base64 encoded hash.</returns>
        private static string ComputeHash(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512);
            var hashBytes = pbkdf2.GetBytes(HashSize);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifies a user's stored credential against the provided password.
        /// </summary>
        /// <param name="user">The user entity.</param>
        /// <param name="password">The plain text password.</param>
        /// <returns>True if the credentials match, otherwise false.</returns>
        public static bool VerifyCredential(User user, string password)
        {
            return user.CurrentCredential != null && VerifyPassword(password, user.CurrentCredential.PasswordHash, user.CurrentCredential.PasswordSalt);
        }

        /// <summary>
        /// Validates the password complexity based on defined security policies.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <exception cref="ArgumentException">Thrown if the password does not meet complexity requirements.</exception>
        private static void ValidatePasswordComplexity(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            if (!HasUpperCase(password) || !HasLowerCase(password) || !HasDigit(password) || !HasSpecialCharacter(password))
                throw new ArgumentException("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
        }

        private static bool HasUpperCase(string input) => input.Any(char.IsUpper);
        private static bool HasLowerCase(string input) => input.Any(char.IsLower);
        private static bool HasDigit(string input) => input.Any(char.IsDigit);
        private static bool HasSpecialCharacter(string input) => input.Any(ch => !char.IsLetterOrDigit(ch));

        /// <summary>
        /// Automatically rehashes a password if the stored hash is outdated (e.g., due to increased security parameters).
        /// </summary>
        /// <param name="user">The user entity.</param>
        /// <param name="password">The plain text password.</param>
        /// <returns>True if the credential was updated, otherwise false.</returns>
        public static bool NeedsRehash(User? user, string password)
        {
            if (user?.CurrentCredential == null)
                return false;

            try
            {
                var saltBytes = Convert.FromBase64String(user.CurrentCredential.PasswordSalt);
                using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA512);
                var hashBytes = pbkdf2.GetBytes(HashSize);

                return Convert.ToBase64String(hashBytes) != user.CurrentCredential.PasswordHash;
            }
            catch
            {
                return true; // If any error occurs (e.g., invalid salt format), assume rehash is needed.
            }
        }
    }
}
