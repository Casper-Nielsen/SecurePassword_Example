namespace SecurePassword_Example.Models
{
    internal class User
    {
        private string username;
        private string password;
        private string salt;

        public string Salt
        {
            get { return salt; }
            set { salt = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
    }
}