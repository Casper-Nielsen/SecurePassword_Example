using Microsoft.AspNetCore.Mvc;
using SecurePassword_Web_Example.Dal;
using SecurePassword_Web_Example.Hashing_Classes;
using SecurePassword_Web_Example.Interfaces;
using SecurePassword_Web_Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private Logic logic;
        private Logic Logic { get { if (logic == null) { logic = new Logic(new Rfc2898DeriveBytesHashing(50000, 64, "sha512"), new DataBaseManager(con)); } return logic; } }

        // POST api/<UserController>
        public bool Post([FromBody] User value)
        {
            return true;
        }


        [HttpPost("createuser")]
        public bool AddUser(User user)
        {
            return Logic.AddUser(user);
        }

        [HttpPost("login")]
        public bool Login(User user)
        {
            return Logic.Login(user);
        }
    }
}
