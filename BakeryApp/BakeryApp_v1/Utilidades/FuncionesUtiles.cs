using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public class FuncionesUtiles : IFuncionesUtiles
{
    public async Task<bool> ConvertirArchivoABytes(Categoria categoria)
    {
        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await categoria.ArchivoCategoria.CopyToAsync(stream);

                categoria.ImagenCategoria = stream.ToArray();
                return true;
            }
        }
        catch (Exception error)
        {
            return false;
        }


    }
}
