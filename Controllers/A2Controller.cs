using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.RegularExpressions;
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
        public ActionResult<string> registerUser(User user)
        {
            User u = _repository.GetUserById(user.UserName);
            if (u == null)
            {
                _repository.AddUser(user);
                return Ok("User successfully registered.");
            }
            
            return Ok($"UserName {user.UserName} is not available.");
        }
        
        // GET /webapi/Donation
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("Donation/{donation}")]
        public ActionResult<string> makeDonation(int donation)
        {
            if (donation <= 0)
            {
                return BadRequest("Amount must be a positive number.");
            }
            DonationCert donationCert = new DonationCert{Amount = donation, UserName = User.Identity.Name};
            return Ok(donationCert);
        }
        
        // POST /webapi/AddEvent
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "StaffOnly")]
        [HttpPost("AddEvent")]
        public ActionResult<string> AddEvent(EventInput eventInput)
        {
            // check for correct date format
            // yyyyMMddTHHmmssZ
            string pattern = @"^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])T([01]\d|2[0-3])[0-5]\d[0-5]\dZ$";
            if (!Regex.IsMatch(eventInput.Start, pattern) && !Regex.IsMatch(eventInput.End, pattern))
            {
                return BadRequest("The format of Start and End should be yyyyMMddTHHmmssZ.");
            }
            else if (!Regex.IsMatch(eventInput.Start, pattern))
            {
                return BadRequest("The format of Start should be yyyyMMddTHHmmssZ.");
            }
            else if (!Regex.IsMatch(eventInput.End, pattern))
            {
                return BadRequest("The format of End should be yyyyMMddTHHmmssZ.");
            }
            
            Event e = new Event
            {
                Description = eventInput.Description,
                Location = eventInput.Location,
                Summary = eventInput.Summary,
                Start = eventInput.Start,
                End = eventInput.End
            };
            _repository.AddEvent(e);
            return Ok("Success");
        }
        
        // GET /webapi/EventCount
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "StaffOnly")]
        [HttpGet("EventCount")]
        public ActionResult<int> countEvents()
        {
            int numOfEvents = _repository.GetAllEvents().Count();
            return Ok(numOfEvents);
        }
    }
}