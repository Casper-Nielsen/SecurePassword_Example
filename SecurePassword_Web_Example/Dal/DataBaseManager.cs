using SecurePassword_Web_Example.Interfaces;
using SecurePassword_Web_Example.Models;
using System.Data;
using System.Data.SqlClient;

namespace SecurePassword_Web_Example.Dal
{
    internal class DataBaseManager : IDataManager
    {
        private string con;

        public DataBaseManager(string con)
        {
            this.con = con;
        }

        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user">the user that will be added</param>
        /// <returns>if the user have been added</returns>
        public bool AddUser(User user)
        {
            bool inserted = false;
            using (SqlConnection con = new SqlConnection(this.con))
            {
                using (SqlCommand cmd = new SqlCommand("SPInsertUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.Username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
                    cmd.Parameters.Add("@salt", SqlDbType.NVarChar).Value = user.Salt;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // gets 0 or 1
                        int input = reader.GetInt32(0);
                        // converts it to a bool
                        inserted = input == 1;
                    }
                    reader.Close();
                    cmd.Dispose();
                }
            }
            return inserted;
        }

        /// <summary>
        /// Gets a user from the username
        /// </summary>
        /// <param name="username">the username of the user that will be returned</param>
        /// <returns>the user with the username</returns>
        public User GetUser(string username)
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(this.con))
            {
                using (SqlCommand cmd = new SqlCommand("SPGetUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // gets the user information
                        user.Username = reader.GetString("Username");
                        user.Password = reader.GetString("Password");
                        user.Salt = reader.GetString("Salt");
                    }
                    reader.Close();
                    cmd.Dispose();
                }
            }
            return user;
        }
    }
}