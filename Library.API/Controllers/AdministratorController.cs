using System;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Entities;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Library.API.Controllers
{
    [Route("Administrator")]
    [ApiController]
    [Authorize]
    public class AdministratorController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdministratorController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> PutUserGradeAsync(Guid userId, byte grade)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (grade is > 4 or 0)
            {
                throw new ArgumentException("Grade不合法，只能设置1,2,3,4");
            }
            user.Grade = grade;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        [HttpPost("GeneralAdministrator")]
        public async Task<IActionResult> AddGeneralAdministratorAsync(UserDto addGeneralAdministrator)
        {
            var generalAdministrator = new User()
            {
                UserName = addGeneralAdministrator.UserName,
                Email = addGeneralAdministrator.Email,
            };
            var result = await _userManager.CreateAsync(generalAdministrator, addGeneralAdministrator.Password);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("Administrator");
                await _userManager.AddToRoleAsync(generalAdministrator, role.Name);
                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault()?.Description);
                return BadRequest(ModelState);
            }
        }
    }
}
