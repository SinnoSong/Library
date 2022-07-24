using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Library.API.Entities;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.API.Controllers;

[Route("auth")]
[ApiController]
[Authorize]
public class AuthenticateController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public AuthenticateController(IConfiguration configuration, UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        Configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IConfiguration Configuration { get; }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateTokenAsync([FromBody] LoginUserDto loginUser)
    {
        var user = await _userManager.FindByEmailAsync(loginUser.UserName);
        if (user == null) return Unauthorized();

        var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUser.Password);
        if (result != PasswordVerificationResult.Success) return Unauthorized();

        var userClaims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        var menuPaths = new HashSet<MenuPath>();
        foreach (var roleItem in userRoles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, roleItem));
            foreach (var item in Configs.MenuPathResource.SetMenu(roleItem)) menuPaths.Add(item);
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };
        claims.AddRange(userClaims);
        var tokenConfigSection = Configuration.GetSection("Security:Token");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigSection["Key"]));
        var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(
            tokenConfigSection["Issuer"],
            tokenConfigSection["Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: signCredential);
        return Ok(new
        {
            userId = user.Id,
            email = user.Email,
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            expiration = TimeZoneInfo.ConvertTime(jwtToken.ValidTo, TimeZoneInfo.Local),
            menuPath = menuPaths
        });
    }

    [HttpPost("register", Name = nameof(AddUserAsync))]
    [AllowAnonymous]
    public async Task<IActionResult> AddUserAsync(RegisterUser registerUser)
    {
        if (registerUser.Grade is > 4 or 0) throw new ArgumentException("Grade不合法，只能设置1,2,3,4");

        var user = new User
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email,
            Grade = registerUser.Grade
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
}