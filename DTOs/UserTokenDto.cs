using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.DTOs
{
    public class UserTokenDto
    {
        public string Email { get; set; }=string.Empty;
        public string Token { get; set; }=string.Empty;
        
    }
}