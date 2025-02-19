using CalculoSeguroVeiculos.Application.Services;
using CalculoSeguroVeiculos.Domain.Interfaces;
using CalculoSeguroVeiculos.Infrastructure.Persistence;
using CalculoSeguroVeiculos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<ISeguroRepository, SeguroRepository>();
builder.Services.AddScoped<SeguroService>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Seguro}/{action=Index}/{id?}");

app.Run();