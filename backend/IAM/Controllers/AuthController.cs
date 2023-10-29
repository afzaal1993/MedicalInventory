using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

            return Ok(ApiResponse<string>.Success());
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Login(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
                }

                var token = Utility.GetToken(authClaims);

                return Ok(ApiResponse<string>.Success(new JwtSecurityTokenHandler().WriteToken(token)));
            }

            return Unauthorized(ApiResponse<string>.Error("Invalid username or password"));
        }
    }
}