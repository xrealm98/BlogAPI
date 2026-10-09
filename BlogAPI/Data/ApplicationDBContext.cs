using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions DbContextOptions) : base(DbContextOptions)
        {
            
        }
        
        public DbSet<Post> Posts {get; set;}
        
        public DbSet<Category> Categories {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      List<IdentityRole> roles = new List<IdentityRole>
      {
        new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN" 
        },
        new IdentityRole
        {
            Name = "User",
            NormalizedName = "USER" 
        }
          
      };
      builder.Entity<IdentityRole>().HasData(roles);
    }

    }
}