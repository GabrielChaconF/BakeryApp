﻿using BakeryApp_v1.Models;
using PagedList;
using Microsoft.EntityFrameworkCore;
using BakeryApp_v1.DTO;
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

    public async Task<IEnumerable<PersonaDTO>> ObtenerTodasLasPersonas(int pagina)
    {
        int numeroDeElementosPorPagina = 10;

        var todasLasPersonas = dbContext.Personas
        .Include(persona => persona.IdRolNavigation)
        .OrderBy(persona => persona.IdPersona)
        .Select(persona => new PersonaDTO
        {
            IdPersona = persona.IdPersona,
            Nombre = persona.Nombre,
            PrimerApellido = persona.PrimerApellido,
            SegundoApellido = persona.SegundoApellido,
            Correo = persona.Correo,
            Telefono = persona.Telefono,
            IdRol = persona.IdRol,
            Rol = new RoleDTO
            {
                IdRol = persona.IdRolNavigation.IdRol,
                NombreRol = persona.IdRolNavigation.NombreRol
            }
        })
        .ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);



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
