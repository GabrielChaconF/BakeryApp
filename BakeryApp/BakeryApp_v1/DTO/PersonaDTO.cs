using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO;

public class PersonaDTO
{
    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contra { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? CodigoRecuperacion { get; set; }

    public int IdRol { get; set; }

    public RoleDTO Rol { get; set; } = null!;


    public static PersonaDTO ConvertirPersonaAPersonaDTO(Persona persona)
    {
        return new PersonaDTO
        {
            IdPersona = persona.IdPersona,
            Correo = persona.Correo,    
            Rol = new RoleDTO
            {
                IdRol = persona.IdRolNavigation.IdRol,
                NombreRol = persona.IdRolNavigation.NombreRol
            }
        };
    }

}
