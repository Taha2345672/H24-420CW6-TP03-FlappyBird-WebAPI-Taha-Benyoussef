using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlappyBird.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class WebAPIContext : IdentityDbContext<User> { 
        public WebAPIContext (DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI.Models.Scores> Scores { get; set; } = default!;
    }
}
