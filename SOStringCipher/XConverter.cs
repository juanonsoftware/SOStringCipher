using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SOStringCipher
{
    /// <summary>
    /// Convert a byte array to string
    /// </summary>
    public static class XConverter
    {
        /// <summary>
        /// Convert an array of bytes to base32 with A-Z0-9 characters
        /// </summary>
        public static string ToBase36(byte[] bytes)
        {
            var dictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return ToBaseN(bytes, dictionary);
        }

        /// <summary>
        /// Convert a hash back to array of bytes
        /// </summary>
        /// <param name="base36Hash">The hash string</param>
        /// <returns></returns>
        public static byte[] FromBase36Hash(string base36Hash)
        {
            var dictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return FromBaseN(base36Hash, dictionary);
        }

        /// <summary>
        /// Convert an array of bytes to base62 with A-Za-z0-9 characters
        /// </summary>
        public static string ToBase62(byte[] bytes)
        {
            var dictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return ToBaseN(bytes, dictionary);
        }

        /// <summary>
        /// Convert a hash back to array of bytes
        /// </summary>
        /// <param name="base62Hash">The hash string</param>
        /// <returns></returns>
        public static byte[] FromBase62Hash(string base62Hash)
        {
            var dictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return FromBaseN(base62Hash, dictionary);
        }

        /// <summary>
        /// Convert an array of bytes to base62 with A-Za-z0-9 characters plus other special characters
        /// </summary>
        /// <param name="specialChars">Additional characters</param>
        public static string ToBase62Plus(byte[] bytes, string specialChars)
        {
            var dictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            dictionary = string.Concat(dictionary, specialChars);
            return ToBaseN(bytes, dictionary);
        }

        /// <summary>
        /// Convert a hash back to array of bytes
        /// </summary>
        /// <param name="base62PlusHash">The hash string</param>
        /// <returns></returns>
        public static byte[] FromBase62PlusHash(string base62PlusHash, string specialChars)
        {
            var dictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            dictionary = string.Concat(dictionary, specialChars);
            return FromBaseN(base62PlusHash, dictionary);
        }

        /// <summary>
        /// Convert a byte array to any base (you define a dictionary of characters)
        /// </summary>
        /// <param name="bytes">Input byte array</param>
        /// <param name="dictionary">The dictionary you defined</param>
        /// <returns>Final string</returns>
        public static string ToBaseN(byte[] bytes, string dictionary)
        {
            int baseN = dictionary.Length;
            var number = new BigInteger(bytes);
            var sb = new StringBuilder();

            while (number != 0)
            {
                BigInteger remainder;
                number = BigInteger.DivRem(number, baseN, out remainder);
                sb.Insert(0, dictionary.ElementAt(Math.Abs((int)remainder)));
            }

            return sb.ToString();
        }

        public static byte[] FromBaseN(string hash, string dictionary)
        {
            int count = hash.Length;
            int baseN = dictionary.Length;
            BigInteger number = 0;

            for (int i = 0; i < count; i++)
            {
                number *= baseN;
                number += (BigInteger)dictionary.IndexOf(hash.ElementAt(i));
            }

            return number.ToByteArray();
        }
    }
}
