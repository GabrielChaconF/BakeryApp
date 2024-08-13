using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloEmpleados")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class IngredienteEmpleadoController : Controller
    {
        private readonly IngredienteService ingredienteService;
        private readonly UnidadMedidaService unidadMedidaService;
        public IngredienteEmpleadoController(IngredienteService ingredienteService, UnidadMedidaService unidadMedidaService)
        {
            this.ingredienteService = ingredienteService;
            this.unidadMedidaService = unidadMedidaService;
        }


        public async Task<IActionResult> Index([FromQuery] int pagina)
        {
            int totalPaginas = await ingredienteService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult AgregarIngrediente()
        {
            return View();
        }

        [HttpGet("/IngredienteEmpleado/ObtenerTodasLasUnidadesDeMedida")]

        public async Task<JsonResult> ObtenerTodasLasUnidadesDeMedida()
        {
            return new JsonResult(new { arregloUnidadesMedida = await  unidadMedidaService.ObtenerTodasLasUnidadesDeMedida()});
        }

        public async Task<IActionResult> EditarIngrediente([FromQuery] int idIngrediente)
        {
            Ingrediente ingredienteEditar = await ingredienteService.ObtenerIngredientePorId(idIngrediente);

            if (ingredienteEditar == null)
            {
                return NotFound();
            }


            return View();
        }

        [HttpGet("/IngredienteEmpleado/ObtenerIngredientes/{pagina}")]
        public async Task<IActionResult> ObtenerIngredientes(int pagina)
        {
            //Si se intenta acceder por URL y por accidente se pone la pagina 0,
            //para que la aplicacion no se caiga
            if (pagina <= 0)
            {
                return BadRequest();
            }

            return new JsonResult(new { arregloIngredientes = await ingredienteService.ObtenerTodasLasIngredientes(pagina) });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarIngrediente([FromBody] Ingrediente ingrediente)
        {
            try
            {
                if (ingrediente == null)
                {
                    return new JsonResult(new { mensaje = "Algunos datos tienen formato incorrecto" });
                }

                if (ingredienteService.VerificarDatosVaciosONulos(ingrediente))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise" });
                }



                bool resultadoRepetida = await ingredienteService.VerificarNombreRepetido(ingrediente);

                if (resultadoRepetida)
                {
                    return new JsonResult(new { mensaje = "El nombre del ingrediente ya se encuentra registrado" });
                }

                if (ingredienteService.VerificarFechaVencimiento(ingrediente))
                {
                    return new JsonResult(new { mensaje = "La fecha de vencimiento del producto no puede negativa fecha actual" });
                }

                if (ingredienteService.VerificarCantidadPositiva(ingrediente))
                {
                    return new JsonResult(new { mensaje = "La cantidad del ingrediente no puede ser negativa" });
                }


                if (ingredienteService.VerificarPrecioPositivo(ingrediente))
                {
                    return new JsonResult(new { mensaje = "El precio del ingrediente no puede ser 0 o negativo" });
                }
                await ingredienteService.Guardar(ingrediente);
                return new JsonResult(new { mensaje = "Ingrediente guardado con éxito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar el ingrediente" });
            }
        }


        [HttpDelete("/IngredienteEmpleado/EliminarIngrediente/{idIngrediente}")]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> EliminarIngrediente(int idIngrediente)
        {
            try
            {



                Ingrediente ingredienteABorrar = await ingredienteService.ObtenerIngredientePorId(idIngrediente);


                await ingredienteService.Eliminar(ingredienteABorrar);

                return new JsonResult(new { mensaje = "Ingrediente eliminado con éxito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar el ingrediente" });
            }
        }


        [HttpGet("/IngredienteEmpleado/DevolverIngredienteEspecifico/{idIngrediente}")]
        public async Task<JsonResult> DevolverIngredienteEspecifico(int idIngrediente)
        {
            IngredienteDTO ingredienteEncontrado = await ingredienteService.ObtenerIngredienteDTOPorId(idIngrediente);
            return new JsonResult(new { ingrediente = ingredienteEncontrado });
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarEditado([FromBody] Ingrediente ingrediente)
        {
            try
            {
                if (ingrediente == null)
                {
                    return new JsonResult(new { mensaje = "Algunos datos tienen formato incorrecto" });
                }

                if (ingredienteService.VerificarDatosVaciosONulos(ingrediente))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise" });
                }



                bool resultadoRepetida = await ingredienteService.VerificarNombreRepetido(ingrediente);

                if (resultadoRepetida)
                {
                    return new JsonResult(new { mensaje = "El nombre del ingrediente ya se encuentra registrado" });
                }

                if (ingredienteService.VerificarFechaVencimiento(ingrediente))
                {
                    return new JsonResult(new { mensaje = "La fecha de vencimiento del producto no puede negativa fecha actual" });
                }

                if (ingredienteService.VerificarCantidadPositiva(ingrediente))
                {
                    return new JsonResult(new { mensaje = "La cantidad del ingrediente no puede ser negativa" });
                }


                if (ingredienteService.VerificarPrecioPositivo(ingrediente))
                {
                    return new JsonResult(new { mensaje = "El precio del ingrediente no puede ser 0 o negativo" });
                }
                await ingredienteService.Editar(ingrediente);
                return new JsonResult(new { mensaje = "Ingrediente modificado con éxito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificiar el ingrediente" });
            }
        }


        [HttpGet]
        public async Task<JsonResult> ObtenerTotalPaginas()
        {
            int totalPaginas = await ingredienteService.CalcularTotalPaginas();
            return new JsonResult(new { paginas = totalPaginas });
        }
    }
}
