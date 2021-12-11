using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.DTOs;
using EmployeeApi.Entities;

namespace EmployeeApi.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<UserDto>> GetEmployees();
        Task<UserDto>  GetEmployeeByEmail(string email);
        Task<UserDto> AddEmployee(UserDto user);
        Task<UserDto> UpdateEmployee(UserDto user);

    }
}