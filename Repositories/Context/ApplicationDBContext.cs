using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Repositories.Context
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
        {
        }

        public ApplicationDBContext()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        }
        public DbSet<Producer> Producer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Event> Event { get; set; }
    }
}
