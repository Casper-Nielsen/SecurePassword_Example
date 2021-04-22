using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecurePassword_Web_Example.View
{
    /// <summary>
    /// This is just for show the api works
    /// </summary>
    public class HTMLViewHolder
    {
        public static string GetLoginView()
        {
            using FileStream stream = new FileStream("./View/LoginPage.html", FileMode.Open);
                using StreamReader readereader = new StreamReader(stream);
                    return readereader.ReadToEnd();
        }
        public static string GetCreateView()
        {
            using FileStream stream = new FileStream("./View/CreateUserPage.html", FileMode.Open);
            using StreamReader readereader = new StreamReader(stream);
            return readereader.ReadToEnd();
        }
        public static string GetHubView()
        {
            using FileStream stream = new FileStream("./View/HubPage.html", FileMode.Open);
            using StreamReader readereader = new StreamReader(stream);
            return readereader.ReadToEnd();
        }
        public static string GetRediret(string address, string message)
        {
            return "<!DOCTYPE html>\n" +
                "<html>\n"+
                "<head> \n" +
                //"<meta http-equiv = \"refresh\" content = \"5; URL=" + address + "\" /> \n" +
                "</head> \n" +
                "<body> \n" +      
                "<h3>"+ message +"</h3>\n" +
                "<p>If you are not redirected in five seconds, <a href = "+ address + ">click here</a>.</p> \n" +
                "</body>\n"+
                "</html>";
        }
    }
}
