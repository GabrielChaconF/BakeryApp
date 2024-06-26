﻿using BakeryApp_v1.Models;
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

        public async Task<IActionResult> Index([FromQuery] int pagina)
        {
            int totalPaginas = await categoriaService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult AgregarCategoria()
        {
            return View();
        }

        //[HttpGet("/Categoria/Pagina/{pagina}")]
        //public IActionResult Pagina(int pagina)
        //{
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> EditarCategoria([FromQuery]int idCategoria)
        {
            Categoria categoriaEditar = await categoriaService.ObtenerCategoriaPorId(idCategoria);

            if (categoriaEditar == null)
            {
                return NotFound();
            }


            return View();
        }

        [HttpGet("/Categoria/ObtenerCategorias/{pagina}")]
        public async Task<IActionResult> ObtenerCategorias(int pagina)
        {
            //Si se intenta acceder por URL y por accidente se pone la pagina 0,
            //para que la aplicacion no se caiga
            if (pagina <= 0)
            {
                return BadRequest();
            }

            return new JsonResult(new { arregloCategorias = await categoriaService.ObtenerTodasLasCategorias(pagina) });
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


        [HttpDelete("/Categoria/EliminarCategoria/{idCategoria}")]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> EliminarCategoria(int idCategoria)
        {
            try
            {
                Categoria categoriaBorrarImagen = await categoriaService.ObtenerCategoriaPorId(idCategoria);

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


        [HttpGet("/Categoria/DevolverCategoriaEspecifica/{idCategoria}")]
        public async Task<JsonResult> DevolverCategoriaEspecifica(int idCategoria)
        {
            Categoria categoriaEncontrada = await categoriaService.ObtenerCategoriaPorId(idCategoria);
            return new JsonResult(new { categoria = categoriaEncontrada });
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarEditada([FromForm] Categoria categoria)
        {


            if (categoriaService.VerificarDatosVaciosONulos(categoria))
            {
                return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
            }


            bool resultadoRepetida = await categoriaService.VerificarNombreRepetido(categoria);

            if (resultadoRepetida)
            {
                return new JsonResult(new { mensaje = "El nombre de la categoria ya se encuentra registrado", correcto = false });
            }

            Categoria categoriaConImagen = await funcionesUtiles.GuardarImagenEnSistemaCategoria(categoria);

            if (categoriaConImagen == null)
            {
                return new JsonResult(new { mensaje = "Error al guardar la imagen", correcto = false });
            }

            await categoriaService.Editar(categoriaConImagen);
            return new JsonResult(new { mensaje = "Categoria modificada con éxito", correcto = true });
        }






        [HttpPost]
        public JsonResult BorrarImagenEditar([FromBody] Categoria categoria)
        {


            try
            {



                if (!funcionesUtiles.BorrarImagenGuardadaEnSistemaCategoria(categoria))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al borrar la imagen", correcto = false });
                }

                return new JsonResult(new { correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }




        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTotalPaginas()
        {
            int totalPaginas = await categoriaService.CalcularTotalPaginas();
            return new JsonResult(new { paginas = totalPaginas });
        }
    }
}
