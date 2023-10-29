using AutoMapper;
using IAM.DTOs;
using IAM.Helpers;
using IAM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<UserDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            if(users == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if(users.Count == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<List<UserDto>>(users);

            return Ok(ApiResponse<List<UserDto>>.Success(dto));
        }

        [HttpGet]
        [Route("GetByUsername/{username}")]
        [ProducesResponseType(typeof(ApiResponse<List<UserDto>>), 200)]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<UserDto>(user);

            return Ok(ApiResponse<UserDto>.Success(dto));
        }

        [HttpPut]
        [Route("Update/{userName}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(string userName, UpdateUserDto model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if(user == null)
                return BadRequest(ApiResponse<string>.Error("Invalid Data"));

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.IsActive = model.IsActive;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(ApiResponse<string>.Success());
            }
            else
                return BadRequest(ApiResponse<string>.Error());
        }
    }
}
