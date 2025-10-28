using Presentation.MiddleWares;
using Presentation.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ServicesCollection();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleWare>();

app.MapControllers();

app.Run();
