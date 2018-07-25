using Framework.Security.Cryptography.Aes;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Framework.Security.Cryptography
{
    /// <summary>
    /// Abtract class for encryption handling.
    /// </summary>
    public abstract class FWEncryption
    {
        /// <summary>
        /// Factory method for the <see cref="FWEncryption"/> class.
        /// </summary>
        /// <returns>A concrete object for encryption handling.</returns>
        public static FWEncryption Create()
        {
            return Create(false);
        }

        /// <summary>
        /// Factory method for the <see cref="FWEncryption"/> class.
        /// </summary>
        /// <param name="useHexadecimal">Use hexadecimal string conversion.</param>
        /// <returns>A concrete object for encryption handling.</returns>
        public static FWEncryption Create(bool useHexadecimal)
        {
            // TODO: If needed, implement a real factory.
            return new FWAesEncryption(useHexadecimal);
        }

        /// <summary>
        /// Encrypts a string.
        /// </summary>
        /// <param name="str">The string to encrypt.</param>
        /// <param name="secureKey">The key used to encrypt the string.</param>
        /// <returns>The ciphered string.</returns>
        public abstract string Encrypt(string str, string secureKey);

        /// <summary>
        /// Decrypts a string.
        /// </summary>
        /// <param name="ciphedStr">The ciphered string.</param>
        /// <param name="secureKey">The key used to encrypt the string.</param>
        /// <returns>The plain string.</returns>
        public abstract string Decrypt(string ciphedStr, string secureKey);
    }
}
