using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class PersonaDAOImpl : PersonaDAO
{
    private readonly BakeryAppContext dbContext;

    public PersonaDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Persona persona) 
    {
        dbContext.Add(persona);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Persona persona)
    {
        dbContext.Update(persona);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Persona persona)
    {
        dbContext.Remove(persona);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Persona> ObtenerPersonaEspecifica(Persona persona)
    {
        Persona personaEncontrada = await dbContext.Personas.FirstOrDefaultAsync(Persona => Persona.IdPersona == Persona.IdPersona);
        return personaEncontrada;
    }

    public async Task<Persona> ObtenerPersonaPorId(int idPersona)
    {
        Persona personaEncontrada = await dbContext.Personas.FirstOrDefaultAsync(Persona => Persona.IdPersona == idPersona);
        return personaEncontrada;
    }

    public async Task<IEnumerable<Persona>> ObtenerTodasLasPersonas(int pagina)
    {
        int numeroDeElementosPorPagina = 10;

        IPagedList<Persona> todasLasPersonas =  dbContext.Personas.OrderBy(Persona => Persona.IdPersona).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);
        return todasLasPersonas;
    }

    public async Task<Persona> ObtenerPersonaPorCorreo(Persona persona)
    {
        Persona personaEncontrada = await dbContext.Personas.FirstOrDefaultAsync(Persona => Persona.Correo == persona.Correo);
        return personaEncontrada;
    }

    public async Task<Persona> ObtenerPersonaPorTelefono(Persona persona)
    {
        Persona personaEncontrada = await dbContext.Personas.FirstOrDefaultAsync(Persona => Persona.Telefono == persona.Telefono);
        return personaEncontrada;
    }


    public async Task<int> ContarTotalPersonas()
    {
        int totalPersonas = await dbContext.Personas.CountAsync();
        return totalPersonas;
    }

}
