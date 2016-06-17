using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TripServer.Models
{
    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasMany(e => e.UpVotes)
                .WithOne(p => p.UpVoteLocation);
            modelBuilder.Entity<Location>()
                .HasMany(e => e.DownVotes)
                .WithOne(p => p.DownVoteLocation);
        }
        
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public Guid Token { get; set; }

        public Location UpVoteLocation { get; set; }
        public Location DownVoteLocation { get; set; }

    }

    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int Nights { get; set; }
        public List<User> UpVotes { get; set; }
        public List<User> DownVotes { get; set; }
    }

    public class Address
    {
        public Guid Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string PostCode { get; set; }
    }
}