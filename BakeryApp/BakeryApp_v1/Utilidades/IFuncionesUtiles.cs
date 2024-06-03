using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public interface IFuncionesUtiles
{
    public Task<bool> ConvertirArchivoABytes(Categoria categoria);
}
