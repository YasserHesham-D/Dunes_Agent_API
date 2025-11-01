using Application.Dtos.Login;
using Domain.Models;
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

    }
}
