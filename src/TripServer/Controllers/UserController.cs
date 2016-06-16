using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripServer.Models;
using TripServer.Security;

namespace TripServer.Controllers
{
    public class UserController : Controller
    {
        private readonly TripContext _context;

        public UserController(TripContext context)
        {
            _context = context;
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
            if (user == null)
                return NotFound();

            var token = Guid.NewGuid();
            Session.TokenDictionary.Add(token, user.Id);
            user.Token = token;
            return Ok(user);
        }

        public class CreateDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string DisplayName { get; set; }
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateDto createDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == createDto.Username && u.Password == createDto.Password);
            if (user != null)
                return BadRequest();

            var token = Guid.NewGuid();

            user = new User
            {
                Id = Guid.NewGuid(),
                DisplayName = createDto.DisplayName,
                Username = createDto.Username,
                Password = createDto.Password,
                Token = token
            };

            Session.TokenDictionary.Add(token, user.Id);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
    }
}
