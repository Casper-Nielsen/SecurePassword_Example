using LiteDB;
using SecurePassword_Web_Example.Interfaces;
using SecurePassword_Web_Example.Models;

namespace SecurePassword_Web_Example.Dal;

public class LiteDbManager : IDataManager
{
    public bool AddUser(User user)
    {
        using var db = new LiteDatabase(@"./MyData.db");
        var col = db.GetCollection<User_dbo>("users");
            
        var userDbo = new User_dbo
        {
            Username = user.Username,
            Password = user.Password,
            Salt = user.Salt
        };
            
        col.Insert(userDbo);
            
        return true;
    }

    public User GetUser(string username)
    {
        using var db = new LiteDatabase(@"./MyData.db");
        
        var col = db.GetCollection<User_dbo>("users");
        
        var userDbo = col.FindOne(x => x.Username == username);
        
        var user = new User
        {
            Username = userDbo.Username,
            Password = userDbo.Password,
            Salt = userDbo.Salt
        };
        
        return user;
    }
}