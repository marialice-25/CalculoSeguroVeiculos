using CalculoSeguroVeiculos.Application.Interfaces;
using CalculoSeguroVeiculos.Application.Services;
using CalculoSeguroVeiculos.Domain.Interfaces;
using CalculoSeguroVeiculos.Infrastructure.Persistence;
using CalculoSeguroVeiculos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISeguroService, SeguroService>();
builder.Services.AddScoped<ISeguroRepository, SeguroRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.MapControllers();
app.Run();