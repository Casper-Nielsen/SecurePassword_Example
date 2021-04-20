using SecurePassword_Web_Example.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace SecurePassword_Web_Example.Hashing_Classes
{
    internal class HmacHashing : IHashing
    {
        private HMAC hmac;

        public HmacHashing(byte[] key, string hashingType)
        {
            SetHashingType(hashingType);
            hmac.Key = key;
        }

        /// <summary>
        /// sets the hashing type
        /// </summary>
        /// <param name="hashingType">the hashing type</param>
        private void SetHashingType(string hashingType)
        {
            // Switch case to find the hashing class
            hmac = hashingType.ToLower() switch
            {
                "sha1" => new HMACSHA1(),
                "sha256" => new HMACSHA256(),
                "sha384" => new HMACSHA384(),
                "sha512" => new HMACSHA512(),
                "md5" => new HMACMD5(),
                _ => new HMACSHA1(),
            };
        }

        /// <summary>
        /// hashes the message with the hashing type that is set using the key
        /// </summary>
        /// <param name="message">the message that will be hashed</param>
        /// <returns>the hashed data</returns>
        public byte[] ComputeMAC(byte[] message, byte[] salt)
        {
            byte[] mac = new byte[0];
            try
            {
                mac = hmac.ComputeHash(Combind(message, salt));
            }
            catch { }
            return mac;
        }

        /// <summary>
        /// looks if the 2 byte arrays is the same
        /// </summary>
        /// <param name="mac1">the first array</param>
        /// <param name="mac2">the second array</param>
        /// <returns>if it is the same</returns>
        public bool Validate(byte[] mac1, byte[] mac2)
        {
            return mac1.SequenceEqual(mac2);
        }

        /// <summary>
        /// Combinds 2 arrays into one
        /// </summary>
        /// <param name="array1">the first array</param>
        /// <param name="array2">the second array</param>
        /// <returns>the full array</returns>
        private byte[] Combind(byte[] array1, byte[] array2)
        {
            byte[] buffer = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array1, 0, buffer, 0, array1.Length);
            Buffer.BlockCopy(array2, 0, buffer, array1.Length, array2.Length);
            return buffer;
        }

        /// <summary>
        /// generates a salt array using RNGCryptoServiceProvider
        /// </summary>
        /// <param name="lenght">the lenght of the salt</param>
        /// <returns>the salt</returns>
        public byte[] GenerateSalt(int lenght)
        {
            byte[] salt = new byte[lenght];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}