using Library.API.Entities;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Library.API.Controllers
{
    // location:9090/auth/user/8CA50E1B-4DC4-421C-BA9C-E1112C782611
    [Route("auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public AuthenticateController(IConfiguration configuration, UserManager<User> userManager,
            RoleManager<Role> roleManager, IMapper mapper)
        {
            Configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IConfiguration Configuration { get; }

        [HttpPost("login")]
        public async Task<IActionResult> GenerateTokenAsync(LoginUserDto loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUser.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var roleItem in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, roleItem));
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            claims.AddRange(userClaims);
            var tokenConfigSection = Configuration.GetSection("Security:Token");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigSection["Key"]));
            var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: tokenConfigSection["Issuer"],
                audience: tokenConfigSection["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signCredential);
            return Ok(new
            {
                userId = user.Id,
                email = user.Email,
                accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = TimeZoneInfo.ConvertTimeFromUtc(jwtToken.ValidTo, TimeZoneInfo.Local)
            });
        }

        [HttpPost("register", Name = nameof(AddUserAsync))]
        public async Task<IActionResult> AddUserAsync(RegisterUser registerUser)
        {
            if (registerUser.Grade is > 4 or 0)
            {
                throw new ArgumentException("Grade不合法，只能设置1,2,3,4");
            }

            var user = new User()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                Grade = registerUser.Grade,
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("User");
                await _userManager.AddToRoleAsync(user, role.Name);
                return Ok();
            }

            ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault()?.Description);
            return BadRequest(ModelState);
        }

        [HttpPut("/user/{id:guid}", Name = nameof(UpdateUserAsync))]
        public async Task<IActionResult> UpdateUserAsync(Guid id, UserUpdateDto userDto)
        {
            // todo 待重新修改
            var userEntity = await _userManager.FindByIdAsync(id.ToString());
            if (userEntity == null)
            {
                throw new Exception("用户不存在！");
            }

            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }

            ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault()?.Description);
            return BadRequest(ModelState);
        }

        [HttpDelete("/user/{id:guid}", Name = nameof(DeleteUserAsync))]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }

            throw new Exception("删除失败！");
        }
    }
}