using Application.Services.AccountServices;
using System.IdentityModel.Tokens.Jwt;

namespace Presentation.MiddleWares
{
    public class TokensBlacklistMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context, ITokenBlackListService blacklistService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwt;

                try
                {
                    jwt = handler.ReadJwtToken(token);
                }
                catch
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }

                var jti = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                if (!string.IsNullOrEmpty(jti) && await blacklistService.IsBlacklistedAsync(jti))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token is blacklisted.");
                    return;
                }
            }

            await next(context);
        }
    }
}
