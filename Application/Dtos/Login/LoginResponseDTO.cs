using Domain.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Login
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string ExpirationDT { get; set; } = string.Empty;

        public RefreshToken RefreshToken { get; set; } = null!;
    }
}
