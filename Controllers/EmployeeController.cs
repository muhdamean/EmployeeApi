using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Entities;
using EmployeeApi.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.Services;
using EmployeeApi.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeApi.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly EmployeeDbContext _context;
        private readonly IEmployeeService employeeService;

        public EmployeeController(UserManager<User> userManager, EmployeeDbContext context,IEmployeeService employeeService)
        {
            _userManager = userManager;
            _context = context;
            this.employeeService = employeeService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            var users=await _userManager.Users.ToListAsync();
            List<UserDto> usersList=new();
            foreach (var user in users)
            {
                UserDto userDto=new UserDto
                {
                    Id=user.Id,
                    Username=user.UserName,
                    Email=user.Email,
                    Phone=user.PhoneNumber,
                };
                usersList.Add(userDto);
            }
            return Ok(usersList);
        }
          [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById(string id)
        {
            var check=await _userManager.FindByIdAsync(id);
            if(check==null) return NotFound("Employee with email not found");
            var user=await employeeService.GetEmployeeByEmail(check.Email);
            if(user==null) return NotFound();
            return Ok(user);
        }
          [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
           var delete=await _userManager.FindByIdAsync(id);
            if(delete==null) return NotFound();
            await _userManager.DeleteAsync(delete);
            return Ok(new {message="Employee deleted successfully"});
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserDto user)
        {
            var check=await _userManager.FindByEmailAsync(user.Email);
            if(check!=null) return BadRequest(new {message="Employee already exists"});
            var createdUser=await employeeService.AddEmployee(user);
            return Ok(createdUser);
        }
          [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id,UserDto user)
        {
            if(id!=user.Id) return BadRequest(new {message="Employee id does not match"});
            var check= await _userManager.FindByIdAsync(user.Id);
            // if(check==null) NotFound("Employee not found");
            // UserDto updatedEmployee=await employeeService.UpdateEmployee(user);

            check.Email=user.Email;
            check.UserName=user.Username;
            check.PhoneNumber=user.Phone;
            await _userManager.UpdateAsync(check);
            return Ok(new {message=$"{check.Email} updated successfully"});
        }
    }
}