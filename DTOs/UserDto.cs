using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.DTOs
{
    public class UserDto
    {
        public string Id {get; set;}=string.Empty;
        public string Username { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string Phone { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        
    }
}