namespace SecurePassword_Web_Example.Models;

public class User_dbo
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}