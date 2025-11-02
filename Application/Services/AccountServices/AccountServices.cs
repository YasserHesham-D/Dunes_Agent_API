using Application.Dtos.Login;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models.Accounts;
using Microsoft.EntityFrameworkCore;
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
    public class AccountServices(IConfiguration configuration,IRefreshToken refreshTokenRepo,IAccountsRepo accountsRepo,IUnitOfWork unitOfWork) : IAccountServices
    {
        public async Task<LoginResponseDTO> Login(Employee employee)
        {
            var Token =  CreateAccessToken(employee);
            var TokenExpTime = configuration.GetSection("JWT").GetSection("LifeTime").Value;

            int x = int.Parse(TokenExpTime);

            var Rtoken = GenerateRefreshToken();
            Rtoken.UserId = employee.Id;

            await refreshTokenRepo.AddAsync(Rtoken);
            await unitOfWork.SaveChangesAsync();

            return new LoginResponseDTO
            {
                Token = Token,
                ExpirationDT = DateTime.UtcNow.AddMinutes(x).ToString() ,
                RefreshToken = Rtoken,
            };
        }

        public async Task<(string Token, RefreshToken RefreshToken)> RefreshTokenAsync(string token)
        {
            var refreshToken = await refreshTokenRepo.GetFirstOrDefaultAsync(x => x.Token == token);

            if (refreshToken == null || !refreshToken.IsActive)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var user = await accountsRepo.GetByIdAsync(refreshToken.UserId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            refreshToken.Revoked = DateTime.UtcNow;
            var newRefresh = GenerateRefreshToken();
            newRefresh.UserId = user.Id;

            await refreshTokenRepo.AddAsync(newRefresh);
            await unitOfWork.SaveChangesAsync();

            var jwtToken = CreateAccessToken(user);
            return (jwtToken, newRefresh);
        }

        public async Task LogoutAsync(string userId)
        {
            var tokens = await refreshTokenRepo.GetAllAsync(t => t.UserId == userId && t.Expires > DateTime.UtcNow );

            foreach (var token in tokens)
               await refreshTokenRepo.DeleteAsync(token.Id);


            await unitOfWork.SaveChangesAsync();


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
        private RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }
    }
}
