﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Semantics;
using Microsoft.EntityFrameworkCore;
using TripServer.Models;
using TripServer.Security;

namespace TripServer.Controllers
{
    public class LocationController : Controller
    {
        private readonly TripContext _context;

        public LocationController(TripContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Locations()
        {
            var locations = await _context.Locations.Include(t => t.DownVotes).Include(t => t.UpVotes).Include(t => t.Address).ToListAsync();
            var locationDtos = locations.Select(t => new LocationDto
            {
                Id = t.Id,
                Address = t.Address?? new Address(),
                ImageUrl = t.ImageUrl,
                Name = t.Name,
                Price = t.Price,
                Nights = t.Nights,
                UpVotes = (t.UpVotes?? new List<User>()).Select(uv => uv.Id).ToList(),
                DownVotes = (t.DownVotes ?? new List<User>()).Select(uv => uv.Id).ToList()
            }).ToList();
            return Ok(locationDtos);
        }

        [HttpPut]
        public IActionResult Location([FromBody]LocationDto locationDto)
        {
            var tokenString = Request.Headers["token"];

            if (String.IsNullOrWhiteSpace(tokenString))
                return Unauthorized();

            Guid token;
            Guid.TryParse(tokenString, out token);

            if (token == null)
                return Unauthorized();

            if (!Session.TokenDictionary.ContainsKey(token))
                return Unauthorized();

            
            var location = locationDto.Id == null ? 
                             new Location() :
                            _context.Locations.Include(t => t.Address).FirstOrDefault(t => t.Id == locationDto.Id.Value);

            if (location == null)
                return NotFound("Location matching Id Not found");

            if (location.Address == null)
                location.Address = locationDto.Address ?? new Address();
            else
            {
                location.Address.Address1 = locationDto.Address.Address1;
                location.Address.Address2 = locationDto.Address.Address2;
                location.Address.Address3 = locationDto.Address.Address3;
                location.Address.Address4 = locationDto.Address.Address4;
                location.Address.Address5 = locationDto.Address.Address5;
                location.Address.PostCode = locationDto.Address.PostCode;
            }
            location.ImageUrl = locationDto.ImageUrl;
            location.Name = locationDto.Name;
            location.Price = locationDto.Price;
            location.Nights = locationDto.Nights;

            if (location.Id == Guid.Empty)
                location.Id = Guid.NewGuid();

            if (location.Address?.Id == Guid.Empty)
                location.Address.Id = Guid.NewGuid();

            if (locationDto.Id == null)
                _context.Locations.Add(location);
            _context.SaveChanges();
            return Ok();
        }

        public class Vote
        {
            public Guid UserId { get; set; }
        }

        [Route("location/{locationId:guid}/upvote")]
        public IActionResult UpVote(Guid locationId, [FromBody]Vote voteDto)
        {
            var tokenString = Request.Headers["token"];

            if (String.IsNullOrWhiteSpace(tokenString))
                return Unauthorized();

            Guid token;
            Guid.TryParse(tokenString, out token);

            if (token == null)
                return Unauthorized();

            if (!Session.TokenDictionary.ContainsKey(token))
                return Unauthorized();

            var location = _context.Locations.Include(t => t.DownVotes).Include(t => t.UpVotes).FirstOrDefault(t => t.Id == locationId);

            if (location == null)
                return NotFound($"Location Id [{locationId}] not found");

            var user = _context.Users.FirstOrDefault(t => t.Id == voteDto.UserId);
            if (user == null)
                return NotFound($"User Id [{voteDto.UserId}] not found");

            if(location.UpVotes == null)
                location.UpVotes = new List<User>();

            if (location.UpVotes.Contains(user))
                location.UpVotes.Remove(user);
            else
            {
                location.UpVotes.Add(user);
            }
            _context.SaveChanges();
            return Ok();
        }

        [Route("location/{locationId:guid}/downvote")]
        public IActionResult DownVote(Guid locationId, [FromBody]Vote voteDto)
        {
            var tokenString = Request.Headers["token"];

            if (String.IsNullOrWhiteSpace(tokenString))
                return Unauthorized();

            Guid token;
            Guid.TryParse(tokenString, out token);

            if (token == null)
                return Unauthorized();

            if (!Session.TokenDictionary.ContainsKey(token))
                return Unauthorized();

            var location = _context.Locations.Include(t => t.DownVotes).Include(t => t.UpVotes).FirstOrDefault(t => t.Id == locationId);

            if (location == null)
                return NotFound($"Location Id [{locationId}] not found");

            var user = _context.Users.FirstOrDefault(t => t.Id == voteDto.UserId);
            if (user == null)
                return NotFound($"User Id [{voteDto.UserId}] not found");

            if (location.DownVotes == null)
                location.DownVotes = new List<User>();

            if (location.DownVotes.Contains(user))
                location.DownVotes.Remove(user);
            else
            {
                location.DownVotes.Add(user);
            }

            
            _context.SaveChanges();
            return Ok();
        }
    }

    public class LocationDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int Nights { get; set; }
        public IList<Guid> UpVotes { get; set; }
        public IList<Guid> DownVotes { get; set; }
    }
}
