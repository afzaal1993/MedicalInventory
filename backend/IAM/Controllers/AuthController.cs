using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAM.Helpers;
using IAM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _config = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Register(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return Conflict(ApiResponse<string>.Error("User already exists"));

            AppUser user = new()
            {
                Email = model.Email,
                UserName = model.Username,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(500, ApiResponse<string>.Error());

            if (model.Role.ToString().ToUpper() == "ADMIN")
            {
                if (!await _roleManager.RoleExistsAsync(UserRole.Admin.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));

                await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
            }
            else
            {
                if (!await _roleManager.RoleExistsAsync(UserRole.User.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole(UserRole.User.ToString()));

                await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
            }

            // https://www.c-sharpcorner.com/article/jwt-authentication-and-authorization-in-net-6-0-with-identity-framework/

            return Ok(ApiResponse<string>.Success());
        }
    }
}