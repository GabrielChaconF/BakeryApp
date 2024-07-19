
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloAdministradores")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class AdministradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //----------------Perfil------------------
        public IActionResult PerfilAdministrador()
        {
            return View();
        }
       
        

        //----------------Facturas------------------
        public IActionResult Facturacion()
        {
            return View();
        }

        public IActionResult Facturas()
        {
            return View();
        }

        public IActionResult VerFactura()
        {
            return View();
        }
        //----------------Pedidos------------------
        public IActionResult Pedidos()
        {
            return View();
        }

        public IActionResult VerPedido()
        {
            return View();
        }


        //----------------Marketing------------------
        public IActionResult Marketing()
        {
            return View();
        }

        public IActionResult EnviarCorreo()
        {
            return View();
        }

        public IActionResult EnviarCorreoTodos()
        {
            return View();
        }


        //----------------Categorias------------------
        public IActionResult Categorias()
        {
            return View();
        }

        public IActionResult EditarCategoria()
        {
            return View();
        }

        public IActionResult AgregarCategoria()
        {
            return View();
        }


    }
}
