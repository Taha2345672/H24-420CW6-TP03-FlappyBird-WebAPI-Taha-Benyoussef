﻿using FlappyBird.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class WebAPIContext : IdentityDbContext<User>
    {
        public WebAPIContext(DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Scores> Scores { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Users
            var user1 = new User
            {
                Id = "user1-id",
                UserName = "User1",
                NormalizedUserName = "USER1",
                Email = "user1@example.com",
                NormalizedEmail = "USER1@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Password123!")
            };

            var user2 = new User
            {
                Id = "user2-id",
                UserName = "User2",
                NormalizedUserName = "USER2",
                Email = "user2@example.com",
                NormalizedEmail = "USER2@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Password123!")
            };

            builder.Entity<User>().HasData(user1, user2);

            // Seed Scores
            builder.Entity<Scores>().HasData(
                new Scores
                {
                    Id = 1,
               
                    Pseudo = "Player1",
                    Date = DateTime.Now,
                    TimeInSeconds = 120,
                    ScoreValue = 100,
                    IsPublic = true
                },
                new Scores
                {
                    Id = 2,
                    Pseudo = "Player1",
                    Date = DateTime.Now,
                    TimeInSeconds = 90,
                    ScoreValue = 50,
                    IsPublic = false
                },
                new Scores
                {
                    Id = 3,
                    Pseudo = "Player2",
                    Date = DateTime.Now,
                    TimeInSeconds = 200,
                    ScoreValue = 200,
                    IsPublic = true
                },
                new Scores
                {
                    Id = 4,
                    Pseudo = "Player2",
                    Date = DateTime.Now,
                    TimeInSeconds = 150,
                    ScoreValue = 75,
                    IsPublic = false
                }
            );
        }
    }
}
