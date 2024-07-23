
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloClientes")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class UsuarioRegistradoController : Controller
    {
        private readonly PersonaService personaService;
        private readonly ProvinciaService provinciaService;
        private readonly CantonService cantonService;
        private readonly DistritoService distritoService;
        private readonly DireccionesService direccionesService;

        public UsuarioRegistradoController(PersonaService personaService, ProvinciaService provinciaService, CantonService cantonService, DistritoService distritoService, DireccionesService direccionesService)
        {
            this.personaService = personaService;
            this.provinciaService = provinciaService;
            this.cantonService = cantonService;
            this.distritoService = distritoService;
            this.direccionesService = direccionesService;
        }


        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult TiendaUsuario()
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

        public IActionResult ServiciosUsuario()
        {
            return View();
        }

        public IActionResult SobreNosotrosUsuario()
        {
            return View();
        }

        public IActionResult ContactoUsuario()
        {
            return View();
        }

        public IActionResult PerfilUsuario()
        {

            
        
            return View();
        }


        [HttpGet]

        public async Task<IActionResult> ObtenerDireccionUsuario()
        {
            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

            Direccionesusuario direccion = new Direccionesusuario
            {
                IdPersona = personaABuscar.IdPersona
            };

            return new JsonResult(new { arregloDirecciones = await direccionesService.ObtenerTodasLasDireccionesPorUsuario(direccion) });
        }




        [HttpDelete("/UsuarioRegistrado/EliminarDireccion/{idDireccion}")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EliminarDireccion(int idDireccion)
        {
            try
            {
                if (!direccionesService.VerificarIdValidoDireccion(idDireccion))
                {
                    return new JsonResult(new { mensaje = "Identificador de la direccion invalido", correcto = false });
                }


                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                Direccionesusuario direccionAEliminar = await direccionesService.ObtenerDireccionPorId(idDireccion);


                if (!direccionesService.VerificarDireccionPerteneceUsuario(direccionAEliminar, personaABuscar))
                {
                    return new JsonResult(new { mensaje = "Esta direccion no le pertenece, por lo que no la puede eliminar", correcto = false });
                }

                TempData["correcto"] = "Direccion eliminada correctamente";
                await direccionesService.Eliminar(direccionAEliminar);

                return new JsonResult(new { mensaje = Url.Action("PerfilUsuario", "UsuarioRegistrado"), correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AgregarDireccion([FromBody] Direccionesusuario direccion)
        {
            try
            {
                if (direccionesService.DatosVaciosONulos(direccion))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }

                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                // Se le asignan las propiedades del usuario logueado a la direccion

                direccion.IdPersona = personaABuscar.IdPersona;


                bool nombreRepetido = await direccionesService.ObtenerDireccionPorNombre(direccion);

                if (nombreRepetido)
                {
                    return new JsonResult(new { mensaje = "El nombre de la direccion ya se encuentra registrado", correcto = false });
                }

                TempData["correcto"] = "Direccion guardada correctamente";
                await direccionesService.Guardar(direccion);
                return new JsonResult(new { mensaje = Url.Action("PerfilUsuario", "UsuarioRegistrado"), correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }

        }



        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarDireccion([FromBody] Direccionesusuario direccion)
        {
            try
            {
                if (direccion == null)
                {
                    return new JsonResult(new { mensaje = "No puedo modificar una direccion, ya que no hay direcciones registradas", correcto = false });
                }

                if (direccionesService.DatosVaciosONulos(direccion))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }

                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                // Se le asignan las propiedades del usuario logueado a la direccion

                direccion.IdPersona = personaABuscar.IdPersona;


                bool nombreRepetido = await direccionesService.ObtenerDireccionPorNombre(direccion);

                if (nombreRepetido)
                {
                    return new JsonResult(new { mensaje = "El nombre de la direccion ya se encuentra registrado", correcto = false });
                }

                TempData["correcto"] = "Direccion modificada correctamente";
                await direccionesService.Editar(direccion);
                return new JsonResult(new { mensaje = Url.Action("PerfilUsuario", "UsuarioRegistrado"), correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }

        }

        [HttpGet]

        public async Task<IActionResult> ObtenerTodasProvincias()
        {
            return new JsonResult(new { arregloProvincias = await provinciaService.ObtenerTodasLasProvincias() });
        }

        [HttpGet("/UsuarioRegistrado/ObtenerTodasLosCantonesPorProvincia/{idProvincia}")]

        public async Task<IActionResult> ObtenerTodasLosCantonesPorProvincia(int idProvincia)
        {
            Provincia provincia = new Provincia
            {
                IdProvincia = idProvincia,
            };
            return new JsonResult(new { arregloCantones = await cantonService.ObtenerTodasLosCantonesPorProvincia(provincia)});
        }


        [HttpGet("/UsuarioRegistrado/ObtenerTodasLosDistritosPorCanton/{idCanton}")]

        public async Task<IActionResult> ObtenerTodasLosDistritosPorCanton(int idCanton)
        {
            Cantone canton = new Cantone
            {
                IdCanton = idCanton,
            };
            return new JsonResult(new { arregloDistritos = await distritoService.ObtenerTodasLosDistritosPorCanton(canton) });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EliminarUsuario([FromBody] Persona persona)
        {
            if (personaService.VerificarContraVacia(persona))
            {
                return new JsonResult(new { mensaje = "La contraseña no puede estar vacia", correcto = false });
            }

            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            Persona personaAEliminar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


            if (!personaService.VerificarContraConContraUsuario(persona, personaAEliminar))
            {
                return new JsonResult(new { mensaje = "Contraseña incorrecta", correcto = false });
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await personaService.Eliminar(personaAEliminar);
            return new JsonResult(new { mensaje = Url.Action("Index", "Home"), correcto = true });
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario([FromBody] Persona persona)
        {
            try
            {
                if (personaService.VerificarNombreApellidosOTelefonoNulos(persona))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }


                bool resultadoRepetidaTelefono = await personaService.VerificarTelefonoRepetido(persona);

                if (resultadoRepetidaTelefono)
                {
                    return new JsonResult(new { mensaje = "El telefono de la persona ya se encuentra registrado", correcto = false });
                }


                if (!personaService.ValidarLongitudTelefono(persona))
                {
                    return new JsonResult(new { mensaje = "La longitud del telefono debe ser igual a 8 caracteres, con un guion", correcto = false });
                }

                if (!personaService.ValidarNumeroTelefono(persona))
                {
                    return new JsonResult(new { mensaje = "El numero de telefono no es valido", correcto = false });
                }

                string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                Persona personaABuscar = new Persona
                {
                    Correo = correoUsuario
                };

                Persona personaAModificar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

                personaAModificar.Nombre = persona.Nombre;
                personaAModificar.PrimerApellido = persona.PrimerApellido;
                personaAModificar.SegundoApellido = persona.SegundoApellido;
                personaAModificar.Telefono = persona.Telefono;

                TempData["correcto"] = "Perfil editado correctamente";
                await personaService.Editar(personaAModificar);

                return new JsonResult(new { mensaje = Url.Action("PerfilUsuario", "UsuarioRegistrado"), correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error inesperado", correcto = false });
            }
        }



        [HttpGet]

        public async Task<IActionResult> ObtenerDatosUsuarioLogueado()
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
                return new JsonResult(new { mensaje = "Ha ocurrido un error al obtener el usuario actual"});
            }
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult Gracias()
        {
            return View();
        }

    }
}
