using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BakeryApp_v1.Services;

public class PersonaServiceImpl : PersonaService
{
    private readonly PersonaDAO personaDAO;

    public PersonaServiceImpl(PersonaDAO personaDAO)
    {
        this.personaDAO = personaDAO;
    }

    public async Task Guardar(Persona persona)
    {
        await personaDAO.Guardar(persona);
    }

    public async Task Editar(Persona persona)
    {
        await personaDAO.Editar(persona);
    }

    public async Task Eliminar(Persona persona)
    {
        await personaDAO.Eliminar(persona);
    }

    public async Task<Persona> ObtenerPersonaEspecifica(Persona persona)
    {
        Persona PersonaBuscada = await personaDAO.ObtenerPersonaEspecifica(persona);
        return PersonaBuscada;
    }

    public async Task<Persona> ObtenerPersonaPorId(int idPersona)
    {
        Persona PersonaBuscada = await personaDAO.ObtenerPersonaPorId(idPersona);
        return PersonaBuscada;
    }

    public async Task<IEnumerable<PersonaDTO>> ObtenerTodasLasPersonas(int pagina)
    {
        IEnumerable<PersonaDTO> todasLasPersonas = await personaDAO.ObtenerTodasLasPersonas(pagina);
        return todasLasPersonas;
    }

    public bool VerificarDatosVaciosONulos(Persona persona)
    {
        if (string.IsNullOrEmpty(persona.Nombre) || string.IsNullOrEmpty(persona.PrimerApellido) || string.IsNullOrEmpty(persona.SegundoApellido) ||
            string.IsNullOrEmpty(persona.Correo) || string.IsNullOrEmpty(persona.Telefono) || persona.IdRol == 0)
        {
            return true;
        }

      

        return false;
    }


    public bool ValidarLongitudContraseña(Persona persona)
    {
        if (persona.Contra.Length < 8)
        {
            return false;
        }

        return true;
    }

    public bool ValidarLongitudTelefono(Persona persona)
    {
        if (persona.Telefono.Length > 9 || persona.Telefono.Length < 9)
        {
            return false;
        }

        return true;
    }

    public bool ValidarNumeroTelefono(Persona persona)
    {
        string patron = @"^[5-9]\d{3}-\d{4}$";

        Regex regexTelefono = new Regex(patron);

        if (!regexTelefono.IsMatch(persona.Telefono))
        {
            return false;
        }
        return true;
    }


    public bool VerificarCorreoElectronico(Persona persona)
    {
        EmailAddressAttribute validacionCorreo = new EmailAddressAttribute();

        if (!validacionCorreo.IsValid(persona.Correo))
        {
            return false;
        }

        return true;
    }



    public async Task<int> CalcularTotalPaginas()
    {
        int totalPersonas = await personaDAO.ContarTotalPersonas();
        int elementosPorPagina = 10;
        double totalPaginas = (double)totalPersonas / elementosPorPagina;
        totalPaginas = Math.Ceiling(totalPaginas);

        return (int)totalPaginas;
    }

    public async Task<bool> VerificarCorreoRepetido(Persona persona)
    {
        Persona estaRepetida = await personaDAO.ObtenerPersonaPorCorreo(persona);

        if (estaRepetida == null)
        {
            return false;
        }

        return true;

    }

    public async Task<bool> VerificarTelefonoRepetido(Persona persona)
    {
        Persona estaRepetida = await personaDAO.ObtenerPersonaPorTelefono(persona);
        if (estaRepetida == null)
        {
            return false;
        }

        return true;
    }



}
