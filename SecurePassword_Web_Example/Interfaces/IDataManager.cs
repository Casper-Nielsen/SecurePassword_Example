using SecurePassword_Web_Example.Models;

namespace SecurePassword_Web_Example.Interfaces
{
    internal interface IDataManager
    {
        bool AddUser(User user);

        User GetUser(string username);
    }
}