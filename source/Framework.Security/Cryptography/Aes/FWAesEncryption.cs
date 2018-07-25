using Framework.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security.Cryptography.Aes
{
    class FWAesEncryption : FWEncryption
    {
        public FWAesEncryption(bool useHexadecimal)
        {
            _useHexadecimal = useHexadecimal;
        }

        public override string Decrypt(string ciphedStr, string secureKey)
        {
            var fullCipher = GetBytes(ciphedStr);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(secureKey);

            using (var aesAlg = System.Security.Cryptography.Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public override string Encrypt(string str, string secureKey)
        {
            var key = Encoding.UTF8.GetBytes(secureKey.ToString());

            using (var aesAlg = System.Security.Cryptography.Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(str);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return GetString(result);
                    }
                }
            }
        }

        private byte[] GetBytes(string message)
        {
            if (_useHexadecimal)
                return FWStringHelper.HexStringToBytes(message);

            return Convert.FromBase64String(message);
        }

        private string GetString(byte[] results)
        {
            if (_useHexadecimal)
                return FWStringHelper.BytesToHexString(results);

            return Convert.ToBase64String(results);
        }

        private bool _useHexadecimal;
    }
}
