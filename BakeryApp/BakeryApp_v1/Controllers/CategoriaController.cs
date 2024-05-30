using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgregarCategoria()
        {
            return View();
        }

        public IActionResult EditarCategoria()
        {
            return View();
        }

    }
}
