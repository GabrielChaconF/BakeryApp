using Microsoft.EntityFrameworkCore;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services.Contrato
{
    public interface IUsuarioService
    {

        Task<Persona> GetPersona(string correo,string clave);
        Task<Persona> SavePersona(Persona modelo);
    }
}
