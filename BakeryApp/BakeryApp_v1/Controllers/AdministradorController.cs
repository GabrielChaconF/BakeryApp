
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloAdministradores")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class AdministradorController : Controller
    {
        private readonly PersonaService personaService;

        public AdministradorController(PersonaService personaService)
        {
            this.personaService = personaService;
        }

        public IActionResult Index()
        {
            return View();
        }
        //----------------Perfil------------------
        public IActionResult PerfilAdministrador()
        {
            return View();
        }




        [HttpGet]

        public async Task<IActionResult> ObtenerDatosAdminLogueado()
        {
            try
            {
                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                Persona personaLogueada = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                return new JsonResult(new { mensaje = PersonaDTO.ConvertirPersonaAPersonaDTOSinRoles(personaLogueada) });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al obtener el usuario actual" });
            }
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
