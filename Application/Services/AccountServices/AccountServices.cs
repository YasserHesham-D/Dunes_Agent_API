using Application.Dtos.Login;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AccountServices
{
    public class AccountServices(IConfiguration configuration) : IAccountServices
    {
        public async Task<LoginResponseDTO> Login(Employee employee)
        {
            var Token =  CreateAccessToken(employee);
            var TokenExpTime = configuration.GetSection("JWT").GetSection("LifeTime").Value;

            int x = int.Parse(TokenExpTime);

            return new LoginResponseDTO
            {
                Token = Token,
                ExpirationDT = DateTime.UtcNow.AddMinutes(x).ToString() 
            };
        }
        private string CreateAccessToken(Employee employee)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, employee.Id),
                new(ClaimTypes.Name, employee.FullName),
                new(ClaimTypes.Email, employee.Email),
                new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var JWT = configuration.GetSection("JWT");
            // SigningCredentials
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (JWT.GetSection("SigningKey").Value));

            var SigningCredential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Token
            var Token = new JwtSecurityToken
            (
                claims: claims,
                issuer: JWT.GetSection("Issuer").Value,
                audience: JWT.GetSection("Audience").Value,
                expires: DateTime.Now.AddMinutes(int.Parse(JWT.GetSection("LifeTime").Value)),
                signingCredentials: SigningCredential
            );

            var JWTTOKEN = new JwtSecurityTokenHandler().WriteToken(Token);

            return JWTTOKEN;
        }
    }
}
