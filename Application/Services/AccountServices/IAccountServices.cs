using Application.Dtos.Login;
using Domain.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AccountServices
{
    public interface IAccountServices
    {
        Task<LoginResponseDTO> Login(Employee employee);
        Task<(string Token, RefreshToken RefreshToken)> RefreshTokenAsync(string token);
        Task LogoutAsync(string userId);
    }
}
