using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Presentation.MiddleWares;
using Presentation.ServiceExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();

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
