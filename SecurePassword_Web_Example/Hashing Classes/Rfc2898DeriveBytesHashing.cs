using SecurePassword_Web_Example.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace SecurePassword_Web_Example.Hashing_Classes
{
    internal class Rfc2898DeriveBytesHashing : IHashing
    {
        private int interations;
        private int hashByteSize;
        private HashAlgorithmName hash;

        public Rfc2898DeriveBytesHashing(int interations, int hashByteSize, string hashingType)
        {
            this.interations = interations;
            this.hashByteSize = hashByteSize;
            SetHashingType(hashingType);
        }

        /// <summary>
        /// sets the hashing type
        /// </summary>
        /// <param name="hashingType">the hashing type</param>
        private void SetHashingType(string hashingType)
        {
            // Switch case to find the hashing class
            hash = hashingType.ToLower() switch
            {
                "sha1" => HashAlgorithmName.SHA1,
                "sha256" => HashAlgorithmName.SHA256,
                "sha384" => HashAlgorithmName.SHA384,
                "sha512" => HashAlgorithmName.SHA512,
                "md5" => HashAlgorithmName.MD5,
                _ => HashAlgorithmName.SHA1,
            };
        }

        /// <summary>
        /// hashes the message with the hashing type
        /// </summary>
        /// <param name="message">the message that will be hashed</param>
        /// <returns>the hashed data</returns>
        public byte[] ComputeMAC(byte[] message, byte[] salt)
        {
            using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(message, salt, interations, hash))
            {
                return hashGenerator.GetBytes(hashByteSize);
            }
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