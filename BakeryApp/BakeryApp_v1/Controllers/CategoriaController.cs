using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Mvc;
using BakeryApp_v1.Utilidades;

namespace BakeryApp_v1.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaService categoriaService;
        private readonly IFuncionesUtiles funcionesUtiles;
        public CategoriaController(CategoriaService categoriaService, IFuncionesUtiles funcionesUtiles)
        {
            this.categoriaService = categoriaService;
            this.funcionesUtiles = funcionesUtiles; 
        }

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

        [HttpPost]

        public async Task<JsonResult> ObtenerCategorias()
        {
            return new JsonResult(new { arregloCategorias = await categoriaService.ObtenerTodasLasCategorias() });
        }


        [HttpPost]

        public async Task<JsonResult> GuardarCategoria([FromForm]Categoria categoria)
        {
            if (categoriaService.VerificarDatosVaciosONulos(categoria))
            {
                return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise"});
            }

            bool resultadoSubida = await funcionesUtiles.ConvertirArchivoABytes(categoria);

            if (!resultadoSubida)
            {
                return new JsonResult(new { mensaje = "Error al convertir la imagen" });
            }

            bool resultadoRepetida = await categoriaService.VerificarNombreRepetido(categoria);

            if (resultadoRepetida)
            {
                return new JsonResult(new { mensaje = "El nombre de la categoria ya se encuentra registrado" });
            }

            await categoriaService.Guardar(categoria);
            return new JsonResult(new{mensaje = "Categoria guardada con éxito"});
        }


    }
}
