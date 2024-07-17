using Microsoft.EntityFrameworkCore;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services.Contrato;

namespace BakeryApp_v1.Services.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly BakeryAppContext _appContext;
        public UsuarioService(BakeryAppContext bakeryAppContext)
        {
            _appContext = bakeryAppContext;
        }
        public async Task<Persona> GetPersona(string correo, string clave)
        {
            Persona persona_encontrada= await _appContext.Personas.Where(u=>u.Correo==correo&&u.Contra==clave).FirstOrDefaultAsync();
            return persona_encontrada;

        }

        public async Task<Persona> SavePersona(Persona modelo)
        {
            _appContext.Personas.Add(modelo);
            await _appContext.SaveChangesAsync();
            return modelo;
        }
    }
}
