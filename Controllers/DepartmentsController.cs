using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Data;
using EmployeeApi.Entities;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
   [Authorize]
    public class DepartmentsController : BaseApiController
    {
        private readonly EmployeeDbContext _context;
        private readonly IDepartmentService departmentService;

        public DepartmentsController(EmployeeDbContext context,IDepartmentService departmentService)
        {
            _context = context;
            this.departmentService = departmentService;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var departments=await departmentService.GetDepartments();
            return Ok(departments);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(int id)
        {
            var department=await departmentService.GetDepartmentById(id);
            if(department==null) return NotFound();
            return Ok(department);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var check= await _context.Departments.FindAsync(id);
            if(check==null) return NotFound();
            var delete= _context.Departments.Remove(check);
            await _context.SaveChangesAsync();
            return Ok(new {message=$"{check.Name} department deleted"});
        }
        [HttpPost]
        public async Task<ActionResult> Create(Department dept)
        {
            var check= await _context.Departments.FindAsync(dept.Id);
            if(check!=null) BadRequest("Department already exists");
            Department createdDept=await departmentService.AddDepartment(dept);
            return Ok(createdDept);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,Department dept)
        {
            if(id!=dept.Id) return BadRequest(new {message="Id mismatch"});
            var check= await _context.Departments.FindAsync(dept.Id);
            if(check==null) BadRequest("Department not found");
            Department updatedDept=await departmentService.UpdateDepartment(dept);
            return Ok(updatedDept);
        }
    }
}