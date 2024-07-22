using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public interface IMailEnviar
{
    public void Configurar();

    public Task<bool> EnviarCorreo(Persona persona, string asunto, string codigoRecuperacion);



}
