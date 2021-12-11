using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Entities;

namespace EmployeeApi.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department>  GetDepartmentById(int id);
        Task<Department> AddDepartment(Department user);
        Task<Department> UpdateDepartment(Department user);

    }
}