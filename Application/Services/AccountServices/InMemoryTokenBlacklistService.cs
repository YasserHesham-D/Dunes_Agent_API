using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AccountServices
{
    public class InMemoryTokenBlacklistService(IMemoryCache cache) : ITokenBlackListService
    {
        public Task AddToBlacklistAsync(string jti, DateTime expiry)
        {
            cache.Set(jti, true, expiry);
            return Task.CompletedTask;
        }

        public Task<bool> IsBlacklistedAsync(string jti)
        {
            return Task.FromResult(cache.TryGetValue(jti, out _));
        }
    }
}
