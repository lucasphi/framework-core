using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Security.Cryptography
{
    /// <summary>
    /// Provides algorithms for hashing.
    /// </summary>
    public static class FWHashProvider
    {
        /// <summary>
        /// Generates a 128-bit salt.
        /// </summary>
        /// <returns>The salt byte array.</returns>
        public static byte[] GenerateSalt()
        {
            return GenerateSalt(16);
        }

        /// <summary>
        /// Generates a salt with a custom size.
        /// </summary>
        /// <param name="size">The salt size in potency of 2.</param>
        /// <returns>The salt byte array.</returns>
        public static byte[] GenerateSalt(int size)
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[size];
            rng.GetBytes(bytes);
            return bytes;
        }

        /// <summary>
        /// Hashes a password using a salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>The hashed password.</returns>
        public static Task<string> HashString(string password, byte[] salt)
        {
            // ref https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing
            var hashed = KeyDerivation.Pbkdf2(
                password, 
                salt, 
                KeyDerivationPrf.HMACSHA512, 
                10000, 
                salt.Length * 2);
            return Task.FromResult(Convert.ToBase64String(hashed));
        }
    }
}
