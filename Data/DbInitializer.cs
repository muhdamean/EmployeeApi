using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EmployeeApi.Entities;

namespace EmployeeApi.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(EmployeeDbContext context, UserManager<User> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user=new User
                {
                    UserName="staff",
                    Email="staff@test.com"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Staff");

                var hod=new User
                {
                    UserName="hod",
                    Email="hod@test.com"
                };
                await userManager.CreateAsync(hod, "Pa$$w0rd");
                await userManager.AddToRoleAsync(hod, "Hod");

                var admin=new User
                {
                    UserName="admin",
                    Email="admin@test.com"
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] {"Staff","Hod","Admin"});
            }
        }
    }
}