using Microsoft.AspNetCore.Mvc;
using SecurePassword_Web_Example.Dal;
using SecurePassword_Web_Example.Hashing_Classes;
using SecurePassword_Web_Example.Interfaces;
using SecurePassword_Web_Example.Models;
using SecurePassword_Web_Example.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurePassword_Web_Example.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        string con = @"Server=(localdb)\MSSQLLocalDB;Database=SecurePassword;User Id=SecurePasswordExecuter;Password=YvbQ3~XDEE#]8GxA";

        private static Logic logic;
        private Logic Logic { 
            get 
            { 
                if (logic == null) 
                { 
                    logic = new Logic(new Rfc2898DeriveBytesHashing(50000, 64, "sha512"), new DataBaseManager(con)); 
                } 
                return logic; 
            } 
        }

        [HttpGet("login")]
        public ContentResult GetLogin()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = HTMLViewHolder.GetLoginView()
            };
        }
        
        [HttpGet("createuser")]
        public ContentResult GetCreateUser()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = HTMLViewHolder.GetCreateView()
            };
        }

        /// <summary>
        /// post call for creating a user
        /// </summary>
        /// <returns>if it is created</returns>
        [HttpPost("createuser")]
        public bool AddUser(User user)
        {
            return Logic.AddUser(user);
        }

        /// <summary>
        /// Post call for login a user in
        /// </summary>
        /// <param name="user">the user that wants to login</param>
        /// <returns>if the user was able to login</returns>
        [HttpPost("login")]
        public bool Login(User user)
        {
            return Logic.Login(user);
        }
        /// <summary>
        /// post call for creating a user using a form
        /// </summary>
        /// <returns>if it is created</returns>
        [HttpPost("Form/createuser")]
        public bool FormAddUser([FromForm] User user)
        {
            return Logic.AddUser(user);
        }

        /// <summary>
        /// Post call for login a user in using a form
        /// </summary>
        /// <param name="user">the user that wants to login</param>
        /// <returns>if the user was able to login</returns>
        [HttpPost("Form/login")]
        public bool FormLogin([FromForm] User user)
        {
            return Logic.Login(user);
        }
    }
}
