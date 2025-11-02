using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AccountServices
{
    public interface ITokenBlackListService
    {
        Task AddToBlacklistAsync(string jti, DateTime expiry);
        Task<bool> IsBlacklistedAsync(string jti);
    }
}
