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
                        cs.Close();
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
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = null;

            if (cipherFormat == OutputFormat.Hex)
            {
                cipherBytes = BigInteger.Parse(cipherText, NumberStyles.HexNumber).ToByteArray();
            }
            else
            {
                cipherBytes = Convert.FromBase64String(cipherText);
            }

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
                        cs.Close();
                    }

                    return Encoding.Unicode.GetString(ms.ToArray());
                }
            }
        }

        private static string ConvertToString(byte[] bytes, OutputFormat outputFormat)
        {
            if (outputFormat == OutputFormat.Hex)
            {
                return new BigInteger(bytes).ToString("X");
            }
            else if (outputFormat == OutputFormat.Base36)
            {
                return XConverter.ToBase36(bytes);
            }
            else if (outputFormat == OutputFormat.Base62)
            {
                return XConverter.ToBase62(bytes);
            }
            else
            {
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
