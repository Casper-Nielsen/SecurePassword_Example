using SecurePassword_Example.Models;

namespace SecurePassword_Example.Interfaces
{
    internal interface IDataManager
    {
        bool AddUser(User user);

        User GetUser(string username);
    }
}