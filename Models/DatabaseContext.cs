using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoursework.Models
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
       
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        { 
               
                base.OnModelCreating(builder);
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Comment> Comments { get; set; }

      

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //    optionsBuilder.UseSqlServer("Data Source = WebCoursework.db");
        // }
    }
}


