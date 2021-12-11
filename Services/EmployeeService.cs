using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Data;
using EmployeeApi.DTOs;
using EmployeeApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext context;
        private readonly UserManager<User> userManager;

        public EmployeeService(EmployeeDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<UserDto> AddEmployee(UserDto user)
        {
            var newUser=new User
            {
                UserName=user.Username,
                Email=user.Email,
                PhoneNumber=user.Phone
            };
            await userManager.CreateAsync(newUser,user.Password);
            await userManager.AddToRoleAsync(newUser,"Staff");
            return user;
        }


        public async Task<UserDto> GetEmployeeByEmail(string email)
        {
            var user=await userManager.FindByEmailAsync(email);            
            var employee=new UserDto
            {
                Id=user.Id,
                Username=user.UserName,
                Email=user.Email,
                Phone=user.PhoneNumber
            };
            return employee;
        }

        public async Task<IEnumerable<UserDto>> GetEmployees()
        {
            var employees= await userManager.Users.ToListAsync();
            List<UserDto> users=new List<UserDto>();
            foreach (var emp in employees)
            {
                UserDto employee=new UserDto
                {
                    Id=emp.Id,
                    Username=emp.UserName,
                    Email=emp.Email,
                    Phone=emp.PhoneNumber
                };
                users.Add(employee);
            }
            return users;
        }

        public async Task<UserDto> UpdateEmployee(UserDto user)
        {
            var update=await userManager.FindByIdAsync(user.Id);
            // var updateUser=new User 
            // {
            //     UserName=user.Username,
            //     Email=user.Email,
            //     PhoneNumber=user.Phone
            // };
            update.UserName=user.Username;
            update.Email=user.Password;
            update.PhoneNumber=user.Phone;            
            var save= await userManager.UpdateAsync(update);
            
            return user;
        }


    }
}