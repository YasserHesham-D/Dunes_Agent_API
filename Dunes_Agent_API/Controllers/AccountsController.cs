using Application.Dtos.Login;
using Application.Services.AccountServices;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(UserManager<Employee> userManager,SignInManager<Employee> signInManager,
                                    RoleManager<IdentityRole> roleManager,IAccountServices accountService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO requestDTO)   // admin123 @dunes.com , Admin@123
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Request");

            var user = await userManager.FindByEmailAsync(requestDTO.Email);
            if (user == null)
                return Unauthorized("Invalid Email Or Password");
            

           
            var passwordValid = await signInManager.CheckPasswordSignInAsync(
                user,
                requestDTO.Password,
                lockoutOnFailure: true
            );

            if (!passwordValid.Succeeded)
            {
                if (passwordValid.IsLockedOut)
                    return Unauthorized("Account temporarily locked due to multiple failed login attempts.");

                return BadRequest("Invalid Email Or Password");
            }

            var AccessToken = await accountService.Login(user);

            return Ok(AccessToken);
        } 

    }
}
