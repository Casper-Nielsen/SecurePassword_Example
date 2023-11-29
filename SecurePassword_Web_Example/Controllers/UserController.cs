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
        private static Logic logic;
        private Logic Logic { 
            get 
            { 
                if (logic == null) 
                { 
                    logic = new Logic(new Rfc2898DeriveBytesHashing(50000, 64, "sha512"), new LiteDbManager()); 
                } 
                return logic; 
            } 
        }

        /// <summary>
        /// gives the html login page that uses the api
        /// </summary>
        /// <returns>the html for the login page</returns>
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

        /// <summary>
        /// gives the html create user page that uses the api
        /// </summary>
        /// <returns>the html for the create user page</returns>
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
        /// gives the html hub page that uses the api
        /// </summary>
        /// <returns>the html for the hub page</returns>
        [HttpGet("hub")]
        public ContentResult GetHub()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = HTMLViewHolder.GetHubView()
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
        public ContentResult FormAddUser([FromForm] User user)
        {
            if (Logic.AddUser(user))
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = HTMLViewHolder.GetRediret("/user/hub", "you have created a accout")
                };
            }
            else
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = HTMLViewHolder.GetRediret("/user/createuser", "invalid input")
                };
            }
        }

        /// <summary>
        /// Post call for login a user in using a form
        /// </summary>
        /// <param name="user">the user that wants to login</param>
        /// <returns>if the user was able to login</returns>
        [HttpPost("Form/login")]
        public ContentResult FormLogin([FromForm] User user)
        {
            if (Logic.Login(user))
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = HTMLViewHolder.GetRediret("/user/hub", "you are logged in")
                };
            }
            else
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = HTMLViewHolder.GetRediret("/user/login", "wrong input")
                };
            }
        }
    }
}
