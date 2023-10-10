using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace SOStringCipher
{
    /// <summary>
    /// Source code is from here https://stackoverflow.com/a/27484425
    /// With a bit improvement
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

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
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

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }

                    return Encoding.Unicode.GetString(ms.ToArray());
                }
            }
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

            return new BigInteger(bytes).ToString("X");
        }
    }
}
