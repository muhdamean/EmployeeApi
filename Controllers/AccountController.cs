using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Data;
using EmployeeApi.DTOs;
using EmployeeApi.Entities;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly EmployeeDbContext _context;
        private readonly TokenService _tokenService;
        public AccountController(UserManager<User> userManager, EmployeeDbContext context, TokenService tokenService)
        {
            _userManager=userManager;
            _context=context;
            _tokenService=tokenService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDto>> Login(LoginDto loginDto)
        {
            var user =await _userManager.FindByEmailAsync(loginDto.Username);
            if(user==null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            return new UserTokenDto
            {
                Email=user.Email,
                Token=await _tokenService.GenerateToken(user)
            };
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserTokenDto>> GetCurrentUser()
        {
            var user=await _userManager.FindByNameAsync(User.Identity.Name);
            return new UserTokenDto
            {
                Email=user.Email,
                Token=await _tokenService.GenerateToken(user)
            };
        }
    }
}