using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.EntityFrameworkCore;
using BakeryApp_v1.Services.Contrato;
using BakeryApp_v1.Services.Implementacion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


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
builder.Services.AddScoped<IngredienteDAO, IngredienteDAOImpl>();
builder.Services.AddScoped<IngredienteService, IngredienteServiceImpl>();
builder.Services.AddScoped<RecetaDAO, RecetaDAOImpl>();
builder.Services.AddScoped<RecetaService, RecetaServiceImpl>();
builder.Services.AddScoped<ProductoDAO, ProductoDAOImpl>();
builder.Services.AddScoped<ProductoService, ProductoServiceImpl>();
builder.Services.AddScoped<IFuncionesUtiles, FuncionesUtiles>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<UnidadMedidaDAO, UnidadMedidaDAOImpl>();
builder.Services.AddScoped<UnidadMedidaService, UnidadMedidaServiceImpl>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => { options.LoginPath = "/Home/IniciarSesion"; options.ExpireTimeSpan = TimeSpan.FromMinutes(20); });




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
app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
