using BakeryApp_v1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BakeryApp_v1.Recursos;
using BakeryApp_v1.Services.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BakeryApp_v1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public HomeController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        /*Layout*/
        public IActionResult Index()
        {
            return View();
        }
        //---------------TIENDA-------------------
        public IActionResult Tienda()
        {
            return View();
        }

        public IActionResult Categorias()
        {
            return View();
        }

        public IActionResult PersonalizarProducto()
        {
            return View();
        }

        public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult SobreNosotros()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {

            Persona persona_Encontrada = await _usuarioService.GetPersona(correo, Recursos.Utilidades.EncriptarClave(clave));
            if (persona_Encontrada == null)
            {
                ViewData["Mensaje"] = "No se encontraron usuarios";
                return View();
            }
            List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name,persona_Encontrada.Nombre)
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registrarse()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registrarse(Persona modelo)
        {
            modelo.Contra = Recursos.Utilidades.EncriptarClave(modelo.Contra);
            Persona persona_Creada = await _usuarioService.SavePersona(modelo);

            if (persona_Creada.IdPersona > 0)

                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "Nose pudo crear el usuario";
            return View();


        }

        public IActionResult RecuperarContrasena()
        {
            return View();
        }

        public IActionResult CambiarContrasena()
        {
            return View();
        }

        public IActionResult Carro()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
