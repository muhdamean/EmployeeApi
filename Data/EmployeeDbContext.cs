using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeApi.Data
{
    public class EmployeeDbContext :IdentityDbContext<User>
    {
        public EmployeeDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Department> Departments{get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                    .HasData(
                        new IdentityRole{Name="Admin", NormalizedName="ADMIN"},
                        new IdentityRole{Name="Hod", NormalizedName="HOD"},
                        new IdentityRole{Name="Staff", NormalizedName="STAFF"}
                    );
        }
    }
}