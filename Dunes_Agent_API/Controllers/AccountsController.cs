using Application.Dtos.Login;
using Application.Services.AccountServices;
using Domain.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(UserManager<Employee> userManager,SignInManager<Employee> signInManager,
                                    IAccountServices accountService, ITokenBlackListService tokenBlackListService) : ControllerBase
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

            var Tokens = await accountService.Login(user);

            return Ok(Tokens);
        }

        [HttpPost]
        [Route("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var result = await accountService.RefreshTokenAsync(refreshToken);
            return Ok(new
            {
                accessToken = result.Token,
                refreshToken = result.RefreshToken.Token
            });
        }

        [HttpPost]
        [Route("[Action]")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("Something Went Wrong ..! ");

            // deleting existed rtokens  // it should be revoked not deleted
            await accountService.LogoutAsync(userId);

            // Blacklist the current access token
            var jti = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            if (!string.IsNullOrEmpty(jti))
            {
                var expClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
                if (!string.IsNullOrEmpty(expClaim) && long.TryParse(expClaim, out var expUnix))
                {
                    var exp = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                    await tokenBlackListService.AddToBlacklistAsync(jti, exp);
                    Log.Information("Access token blacklisted for user: {UserId}", userId);
                }
            }

            return Ok("Logged out successfully");
        }
    }
}
