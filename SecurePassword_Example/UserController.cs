using SecurePassword_Example.Hashing_Classes;
using SecurePassword_Example.Interfaces;
using SecurePassword_Example.Models;
using System;
using System.Text;

namespace SecurePassword_Example
{
    internal class UserController
    {
        private IHashing hashing;
        private IDataManager dataManager;

        public UserController(IHashing hashing, IDataManager dataManager)
        {
            this.hashing = hashing;
            this.dataManager = dataManager;
            this.hashing = new HmacHashing(Encoding.UTF8.GetBytes("hello world"), "sha512");
        }

        /// <summary>
        /// hashes the password, then adds the user to the database
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <param name="password">the password of the user</param>
        /// <returns></returns>
        public bool AddUser(string username, string password)
        {
            if (password.Length > 5)
            {
                byte[] passwordByte = Encoding.UTF8.GetBytes(password);
                byte[] salt = hashing.GenerateSalt(32);
                password = Convert.ToBase64String(hashing.ComputeMAC(passwordByte, salt));
                User user = new User();
                user.Password = password;
                user.Username = username;
                user.Salt = Convert.ToBase64String(salt);
                return dataManager.AddUser(user);
            }
            return false;
        }

        /// <summary>
        /// tries to log the user in
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <param name="password">the password for the user</param>
        /// <returns>if it was able to login</returns>
        public bool Login(string username, string password)
        {
            if (password.Length > 5)
            {
                byte[] passwordByte = Encoding.UTF8.GetBytes(password);
                User user = dataManager.GetUser(username);
                if (user.Password != null)
                {
                    byte[] salt = Convert.FromBase64String(user.Salt);
                    password = Convert.ToBase64String(hashing.ComputeMAC(passwordByte, salt));
                    return hashing.Validate(Convert.FromBase64String(user.Password), Convert.FromBase64String(password));
                }
            }
            return false;
        }
    }
}