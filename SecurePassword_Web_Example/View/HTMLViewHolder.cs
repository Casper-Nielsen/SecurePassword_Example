using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecurePassword_Web_Example.View
{
    public class HTMLViewHolder
    {
        public static string GetLoginView()
        {
            // C:\Users\casp8660\source\repos\SecurePassword_Example\SecurePassword_Web_Example\View\LoginPage.html
            using FileStream stream = new FileStream("./View/LoginPage.html", FileMode.Open);
                using StreamReader readereader = new StreamReader(stream);
                    return readereader.ReadToEnd();
        }
        public static string GetCreateView()
        {
            // C:\Users\casp8660\source\repos\SecurePassword_Example\SecurePassword_Web_Example\View\LoginPage.html
            using FileStream stream = new FileStream("./View/CreateUserPage.html", FileMode.Open);
            using StreamReader readereader = new StreamReader(stream);
            return readereader.ReadToEnd();
        }
    }
}
