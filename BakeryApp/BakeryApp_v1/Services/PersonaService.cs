using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface PersonaService
{
    public Task Guardar(Persona persona);

    public Task Editar(Persona persona);


    public Task Eliminar(Persona persona);

    public Task<Persona> ObtenerPersonaEspecifica(Persona persona);

    public Task<Persona> ObtenerPersonaPorId(int idPersona);

    public bool VerificarDatosVaciosONulos(Persona persona);

    public bool ValidarLongitudContraseña(Persona persona);

    public Task<IEnumerable<Persona>> ObtenerTodasLasPersonas(int pagina);

    public Task<bool> VerificarCorreoRepetido(Persona persona);

    public Task<bool> VerificarTelefonoRepetido(Persona persona);

    public Task<int> CalcularTotalPaginas();

    public bool ValidarLongitudTelefono(Persona persona);
    public bool ValidarNumeroTelefono(Persona persona);
}
