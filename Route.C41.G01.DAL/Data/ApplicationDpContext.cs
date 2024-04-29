using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Route.C41.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.DAL.Data
{
    public class ApplicationDpContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDpContext(DbContextOptions<ApplicationDpContext> options):base (options)
        {
            
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  => optionsBuilder.UseSqlServer("Server =. ; Database = MVCApplication; Trusted_Connection = True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; } 

    }
}
