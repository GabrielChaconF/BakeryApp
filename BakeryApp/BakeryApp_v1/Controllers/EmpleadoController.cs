using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloEmpleados")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class EmpleadoController : Controller
    {

        private readonly PersonaService personaService;

        public EmpleadoController(PersonaService personaService)
        {
            this.personaService = personaService;
        }



        [HttpGet]

        public async Task<IActionResult> ObtenerDatosEmpleadoLogueado()
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


        public IActionResult Index()
        {
            return View();
        }

        //----------------Perfil------------------
        public IActionResult PerfilEmpleado()
        {
            return View();
        }
    
      

        

    }
}
