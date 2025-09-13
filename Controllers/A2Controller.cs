using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using A2Template.Models;
using A2Template.Data;
using A2Template.Dtos;

namespace A2Template.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class A2Controller : Controller
    {
        private readonly IA2Repo _repository;

        public A2Controller(IA2Repo repository)
        {
            _repository = repository;
        }
        
        // POST /webapi/Register
        [HttpPost("Register")]
        public ActionResult<User> registerUser(User user)
        {
            User u = _repository.GetUserById(user.UserName);
            if (u == null)
            {
                _repository.AddUser(user);
                return Ok("User successfully registered.");
            }
            else
            {
                return Ok($"UserName {user.UserName} is not available.");
            }
        }
        
    }
}