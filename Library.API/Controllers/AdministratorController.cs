using Library.API.Entities;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Library.API.Controllers
{
    [Route("Administrator")]
    [ApiController]
    [Authorize]
    public class AdministratorController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdministratorController(IConfiguration configuration, RoleManager<Role> roleManager,UserManager<User> userManager)
        {
            Configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IConfiguration Configuration { get; }
        
        [HttpPost("put")]
         public async Task<IActionResult> PutUserGreadAsync(PutUserGreadDto putUserGread)
        {
            var user = await _userManager.FindByEmailAsync(putUserGread.UserName);
            if (putUserGread.Grade is > 4 or 0)
            {
                throw new ArgumentException("Grade不合法，只能设置1,2,3,4");
            }
            user.Grade = putUserGread.Grade;
            await _userManager.UpdateAsync(user);
            return Ok();    
        }
        [HttpPost("AddGeneralAdministrator")]
        public async Task<IActionResult> AddGeneralAdministratorAsync(AddGeneralAdministratorDto addGeneralAdministrator)
        {
            var GeneralAdministrator = new User()
            {
                UserName = addGeneralAdministrator.UserName,
                Email = addGeneralAdministrator.Email,
            };
            var result = await _userManager.CreateAsync(GeneralAdministrator, addGeneralAdministrator.Password);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("Administrator");
                await _userManager.AddToRoleAsync(GeneralAdministrator, role.Name);
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
