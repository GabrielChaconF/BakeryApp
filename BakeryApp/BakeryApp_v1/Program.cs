using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<BakeryAppContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("conexion")));

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});


builder.Services.AddScoped<CategoriaDAO, CategoriaDAOImpl>();
builder.Services.AddScoped<CategoriaService, CategoriaServiceImpl>();
builder.Services.AddScoped<PersonaDAO, PersonaDAOImpl>();
builder.Services.AddScoped<PersonaService, PersonaServiceImpl>();
builder.Services.AddScoped<RolDAO, RolDAOImpl>();
builder.Services.AddScoped<RolService, RolServiceImpl>();
builder.Services.AddScoped<IFuncionesUtiles, FuncionesUtiles>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
