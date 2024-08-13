using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface ProductoModificadoDAO
{
    public Task Guardar(Productosmodificado producto);

    public Task Editar(Productosmodificado producto);


    public Task Eliminar(Productosmodificado producto);

    public Task<Productosmodificado> ObtenerProductoEspecifico(Productosmodificado producto);

    public Task<Productosmodificado> ObtenerProductoPorId(int idProductoModificado);

  

}
