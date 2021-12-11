using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeApi.Data;
using EmployeeApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeDbContext context;
        private readonly UserManager<User> userManager;

        public DepartmentService(EmployeeDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<Department> AddDepartment(Department dept)
        {
            var department=new Department
            {
                Name=dept.Name,
                Description=dept.Description,
                Date=dept.Date
            };
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();
            return department; 
        }
        public async Task<Department> GetDepartmentById(int id)
        {
            var department=await context.Departments.FirstOrDefaultAsync(x=>x.Id==id);
            return department;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var department=await context.Departments.ToListAsync();
            return department;
        }

        public async Task<Department> UpdateDepartment(Department dept)
        {
            var update=await context.Departments.FirstOrDefaultAsync(x=>x.Id== dept.Id);
            update.Name=dept.Name;
            update.Description=dept.Description;
            update.Date=dept.Date;
            context.Departments.Update(update);
            await context.SaveChangesAsync();
            return update;
        }
    }
}