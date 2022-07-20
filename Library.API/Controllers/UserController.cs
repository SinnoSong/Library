using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public UserController(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPut("{userId:guid}")]
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public async Task<IActionResult> UpdateUserGradeAsync(Guid userId, byte grade)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (grade is > 4 or 0) throw new ArgumentException("Grade不合法，只能设置1,2,3,4");

        user.Grade = grade;
        await _userManager.UpdateAsync(user);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public async Task<List<UserDto>> GetUsersAsync(byte? grade, bool isAdmin, int page = 1, int size = 25)
    {
        if (grade is > 4 or < 0)
        {
            throw new ArgumentException("Grade不合法，只能设置1,2,3,4");
        }

        var users = !isAdmin
            ? _userManager.Users.Where(user => user.Grade != null && user.Grade == grade)
            : _userManager.Users.Where(user => user.Grade == null);

        return await PagedList<UserDto>.CreateAsync(_mapper.ProjectTo<UserDto>(users), page, size);
    }

    [HttpPost("Administrator")]
    [Authorize(Roles = "SuperAdministrator")]
    public async Task<IActionResult> AddGeneralAdministratorAsync(RegisterAdmin addGeneralAdministrator)
    {
        var generalAdministrator = new User
        {
            UserName = addGeneralAdministrator.UserName,
            Email = addGeneralAdministrator.Email
        };
        var result = await _userManager.CreateAsync(generalAdministrator, addGeneralAdministrator.Password);
        if (result.Succeeded)
        {
            var role = await _roleManager.FindByNameAsync("Administrator");
            await _userManager.AddToRoleAsync(generalAdministrator, role.Name);
            return Ok();
        }

        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault()?.Description);
        return BadRequest(ModelState);
    }


    [HttpPut("/changePassword/{id:guid}")]
    public async Task<IActionResult> UpdateUserPasswordAsync(Guid id, UserPasswordChangeDto userPasswordChangeDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var token = new JwtSecurityToken(Request.Headers.Authorization[0]["Bearer ".Length..]);
        var userName = token.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)!.Value;
        if (userName != user.UserName) throw new Exception("请输入自己的ID");

        if (user == null) throw new Exception("此用户不存在");

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userPasswordChangeDto.NewPassword);
        await _userManager.UpdateAsync(user);
        return NoContent();
    }

    [HttpPut("/changeEmail/{id:guid}")]
    public async Task<IActionResult> UpdateUserEmailAsync(Guid id, string newEmail)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) throw new Exception("此用户不存在");

        user.Email = newEmail;
        await _userManager.UpdateAsync(user);
        return Ok();
    }
}