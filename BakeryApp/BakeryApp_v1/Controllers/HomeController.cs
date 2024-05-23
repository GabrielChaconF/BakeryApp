using BakeryApp_v1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BakeryApp_v1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

		public IActionResult RegistrarUsuario()
		{
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
