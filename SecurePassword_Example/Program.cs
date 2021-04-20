using SecurePassword_Example.Dal;
using SecurePassword_Example.Hashing_Classes;
using System;
using System.Threading;

namespace SecurePassword_Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // @"Server=(localdb)\MSSQLLocalDB;Database=SecurePassword;User Id=SecurePasswordExecuter;Password=YvbQ3~XDEE#]8GxA"
            string con = @"Server=(localdb)\MSSQLLocalDB;Database=SecurePassword;User Id=SecurePasswordExecuter;Password=YvbQ3~XDEE#]8GxA";

            //UserController userController = new UserController(new HmacHashing(Encoding.UTF8.GetBytes("Hello World"), "sha512"), new DataBaseManager(con));

            UserController userController = new UserController(new Rfc2898DeriveBytesHashing(50000, 64, "sha512"), new DataBaseManager(con));
            do
            {
                Console.WriteLine("Create user ((true)/false)");
                byte count = 0;
                bool createUser;
                string username;
                string password;
                bool valid = false;
                bool inputright = bool.TryParse(Console.ReadLine(), out createUser);
                if (!inputright)
                {
                    createUser = true;
                }
                do
                {
                    Console.Clear();
                    Console.WriteLine("username: ");
                    username = Console.ReadLine();
                    Console.WriteLine("password: ");
                    password = Console.ReadLine();
                    if (createUser)
                    {
                        valid = userController.AddUser(username, password);
                    }
                    else
                    {
                        valid = userController.Login(username, password);
                        if (!valid)
                        {
                            count++;
                            Console.WriteLine("Wrong");
                            if (count > 6)
                            {
                                for (int i = 25; i > 0; i--)
                                {
                                    Console.Clear();
                                    Console.WriteLine("wait " + i + "sec");
                                    Thread.Sleep(1000);
                                }
                            }
                            else if (count > 3)
                            {
                                for (int i = 5; i > 0; i--)
                                {
                                    Console.Clear();
                                    Console.WriteLine("wait " + i + "sec");
                                    Thread.Sleep(1000);
                                }
                            }
                            else
                            {
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            count = 0;
                        }
                    }
                } while (!valid);
                Console.WriteLine("Welcome " + username);
                Console.WriteLine("type end to exit the program");
            } while (Console.ReadLine() != "end");
        }
    }
}