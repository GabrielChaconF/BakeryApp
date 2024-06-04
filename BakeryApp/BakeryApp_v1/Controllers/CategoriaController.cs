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
        
        public IActionResult EditarCategoria([FromQuery]int idCategoria)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerCategorias()
        {
            return new JsonResult(new { arregloCategorias = await categoriaService.ObtenerTodasLasCategorias() });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarCategoria([FromForm] Categoria categoria)
        {
            try
            {

                if (categoriaService.VerificarDatosVaciosONulos(categoria))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise" });
                }



                bool resultadoRepetida = await categoriaService.VerificarNombreRepetido(categoria);

                if (resultadoRepetida)
                {
                    return new JsonResult(new { mensaje = "El nombre de la categoria ya se encuentra registrado" });
                }

                Categoria categoriaConImagen = await funcionesUtiles.GuardarImagenEnSistemaCategoria(categoria);

                if (categoriaConImagen == null)
                {
                    return new JsonResult(new { mensaje = "Error al guardar la imagen" });
                }

                await categoriaService.Guardar(categoriaConImagen);
                return new JsonResult(new { mensaje = "Categoria guardada con éxito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar la categoria" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> EliminarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                Categoria categoriaBorrarImagen = await categoriaService.ObtenerCategoriaEspecifica(categoria);

                if (!funcionesUtiles.BorrarImagenGuardadaEnSistemaCategoria(categoriaBorrarImagen))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al borrar la imagen" });
                }
                await categoriaService.Eliminar(categoriaBorrarImagen);
                return new JsonResult(new { mensaje = "Categoria eliminada con éxito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar la categoria" });
            }
        }

        


    }
}
