using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.MiddleWares;
using Presentation.ServiceExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();

//builder.Services.AddDbContext<AppDbContext>( options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Yasser's")));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Adhams's")));

// migration command : Add-Migration InitialCreate -Project Infrastructure -StartupProject Presentation -OutputDir Migrations
//                      update-database -startupproject Presentation

builder.Services.AddIdentity<Employee, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.ServicesCollection();

builder.Services.AddControllers();

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

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleWare>();

app.MapControllers();

app.Run();
