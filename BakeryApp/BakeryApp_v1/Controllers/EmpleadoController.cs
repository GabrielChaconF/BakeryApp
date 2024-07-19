using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloEmpleados")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //----------------Perfil------------------
        public IActionResult PerfilEmpleado()
        {
            return View();
        }
        //----------------Inventario------------------
        public IActionResult Inventario()
        {
            return View();
        }

        public IActionResult EditarInventario()
        {
            return View();
        }

        public IActionResult AgregarInventario()
        {
            return View();
        }
        //----------------Recetas------------------
        public IActionResult Recetas()
        {
            return View();
        }

        //----------------Productos------------------
        public IActionResult Productos()
        {
            return View();
        }

        public IActionResult EditarProducto()
        {
            return View();
        }

        public IActionResult AgregarProducto()
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
