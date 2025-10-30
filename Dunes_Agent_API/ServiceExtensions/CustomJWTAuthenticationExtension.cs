using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentation.ServiceExtensions
{
    public class JWTOptions
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Signingkey { get; set; } = null!;
        public double Lifetime { get; set; }

    }

    public static class CustomJWTAuthenticationExtension
    {

        public static void CustomJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jWT = new JWTOptions();
            configuration.GetSection("Jwt").Bind(jWT);

            services.AddSingleton(jWT);

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(Options =>
            {
                Options.RequireHttpsMetadata = false;

                Options.SaveToken = true;

                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jWT.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jWT.Audience,

                    ValidateLifetime = true,
                   
                    ClockSkew = TimeSpan.Zero, 

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT.Signingkey)),
                    RequireExpirationTime = true,
                    RequireSignedTokens = true
                };

                Options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully");
                        return Task.CompletedTask;
                    }
                };

            });
        }
    }

}
