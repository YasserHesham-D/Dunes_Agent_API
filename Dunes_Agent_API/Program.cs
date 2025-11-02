using Application.Validators;
using Domain.Models.Accounts;
using FluentValidation;
using Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.MiddleWares;
using Presentation.ServiceExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Yasser's")));

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Adhams's")));

// migration command : Add-Migration InitialCreate -Project Infrastructure -StartupProject Presentation -OutputDir Migrations
//                      update-database -startupproject Presentation

builder.Services.AddIdentity<Employee, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ServicesCollection();

builder.Services.AddControllers();


builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.CustomJwtAuth(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<TokensBlacklistMiddleware>();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleWare>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<Employee>>();

        await SeedRolesHelper.SeedRolesAsync(roleManager);
        //await SeedRolesHelper.SeedAdminAsync(userManager, roleManager);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✅ Roles and admin user seeded successfully!");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ Error seeding roles or admin user: {ex.Message}");
        Console.ResetColor();
    }
}

app.Run();
