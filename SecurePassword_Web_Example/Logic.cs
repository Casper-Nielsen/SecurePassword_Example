using Microsoft.AspNetCore.Mvc;
using SecurePassword_Web_Example.Dal;
using SecurePassword_Web_Example.Hashing_Classes;
using SecurePassword_Web_Example.Interfaces;
using SecurePassword_Web_Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurePassword_Web_Example
{
    class Logic
    {

        private IHashing hashing;
        private IDataManager dataManager;

        public Logic(IHashing hashing, IDataManager dataManager)
        {
            this.hashing = hashing;
            this.dataManager = dataManager;
            this.hashing = new HmacHashing(Encoding.UTF8.GetBytes("hello world"), "sha512");
        }


        /// <summary>
        /// hashes the password, then adds the user to the database
        /// </summary>
        /// <param name="user">the user that will be added</param>
        /// <returns>if the user got added</returns>
        public bool AddUser(User user)
        {
            if (user.Password.Length > 5)
            {
                byte[] passwordByte = Encoding.UTF8.GetBytes(user.Password);
                byte[] salt = hashing.GenerateSalt(32);
                user.Password = Convert.ToBase64String(hashing.ComputeMAC(passwordByte, salt));
                user.Salt = Convert.ToBase64String(salt);
                return dataManager.AddUser(user);
            }
            return false;
        }

        /// <summary>
        /// tries to log the user in
        /// </summary>
        /// <param name="user">the user that will be validated on</param>
        /// <returns>if it was able to login</returns>
        public bool Login(User user)
        {
            if (user.Password.Length > 5)
            {
                byte[] passwordByte = Encoding.UTF8.GetBytes(user.Password);
                user = dataManager.GetUser(user.Username);
                if (user.Password != null)
                {
                    byte[] salt = Convert.FromBase64String(user.Salt);
                    string password = Convert.ToBase64String(hashing.ComputeMAC(passwordByte, salt));
                    return hashing.Validate(Convert.FromBase64String(user.Password), Convert.FromBase64String(password));
                }
            }
            return false;
        }
    }
}
