using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fanfic.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<User>
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tale>()
                .Property(c => c.Ganre)
                .HasConversion<string>();            

            base.OnModelCreating(builder);
        }

        public DbSet<Tale> Tales { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
