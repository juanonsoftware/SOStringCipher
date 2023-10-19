using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace SOStringCipher
{
    /// <summary>
    /// Original source code is from here https://stackoverflow.com/a/27484425
    /// But with improvements added:
    /// - Use a password as parameter
    /// - Support several kinds of output including HEX, Base64, Base62, Base36
    /// </summary>
    public static class StringCipher
    {
        public static string Encrypt(string clearText, string password)
        {
            return Encrypt(clearText, password, OutputFormat.Base64);
        }

        public static string Encrypt(string clearText, string password, OutputFormat outputFormat)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetPasswordBytes(password);
                aes.IV = GetIVBytes();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }

                    return ConvertToString(ms.ToArray(), outputFormat);
                }
            }
        }

        public static string Decrypt(string cipherText, string password)
        {
            return Decrypt(cipherText, password, OutputFormat.Base64);
        }

        public static string Decrypt(string cipherText, string password, OutputFormat cipherFormat)
        {
            byte[] cipherBytes = ConvertToBytes(cipherText, cipherFormat);

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetPasswordBytes(password);
                aes.IV = GetIVBytes();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }

                    return Encoding.Unicode.GetString(ms.ToArray());
                }
            }
        }

        private static byte[] GetPasswordBytes(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            // Validation of password
            // Valid key size is 128 / 192 / 256 bits
            if (password.Length > 32)
            {
                throw new ArgumentException("Password id too long, it should be max 32 bytes");
            }

            if (password.Length < 32)
            {
                Array.Resize(ref bytes, 32);
            }

            return bytes;
        }

        private static byte[] GetIVBytes()
        {
            return new byte[16];
        }

        private static byte[] ConvertToBytes(string cipherText, OutputFormat cipherFormat)
        {
            if (cipherFormat == OutputFormat.Base64)
            {
                return Convert.FromBase64String(cipherText);
            }

            if (cipherFormat == OutputFormat.Base36)
            {
                return XConverter.FromBase36Hash(cipherText);
            }

            if (cipherFormat == OutputFormat.Base62)
            {
                return XConverter.FromBase62Hash(cipherText);
            }

            // HEX
            return BigInteger.Parse(cipherText, NumberStyles.HexNumber).ToByteArray();
        }

        private static string ConvertToString(byte[] bytes, OutputFormat outputFormat)
        {
            if (outputFormat == OutputFormat.Base64)
            {
                return Convert.ToBase64String(bytes);
            }

            if (outputFormat == OutputFormat.Base36)
            {
                return XConverter.ToBase36(bytes);
            }

            if (outputFormat == OutputFormat.Base62)
            {
                return XConverter.ToBase62(bytes);
            }

            // HEX
            return new BigInteger(bytes).ToString("X");
        }
    }
}
