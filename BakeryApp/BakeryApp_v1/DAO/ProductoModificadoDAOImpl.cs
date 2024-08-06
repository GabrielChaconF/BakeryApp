using BakeryApp_v1.Models;
using System.Data.Entity;

namespace BakeryApp_v1.DAO;

public class ProductoModificadoDAOImpl : ProductoModificadoDAO
{
    private readonly BakeryAppContext dbContext;

    public ProductoModificadoDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext; 
    }

    public async Task Editar(Productosmodificado producto)
    {
        dbContext.Update(producto);
        await dbContext.SaveChangesAsync();
    }

    public async Task Eliminar(Productosmodificado producto)
    {
      
        dbContext.Remove(producto);
        await dbContext.SaveChangesAsync();
    }

    public async Task Guardar(Productosmodificado producto)
    {
        dbContext.Add(producto);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Productosmodificado> ObtenerProductoEspecifico(Productosmodificado producto)
    {

        Productosmodificado productoBuscado = await dbContext.Productosmodificados.FirstOrDefaultAsync(Producto => Producto.IdProductoModificado == producto.IdProductoModificado);
        return productoBuscado;
    }

    public async Task<Productosmodificado> ObtenerProductoPorId(int idProductoModificado)
    {
        Productosmodificado productoBuscado = await dbContext.Productosmodificados.FirstOrDefaultAsync(Producto => Producto.IdProductoModificado == idProductoModificado);
        return productoBuscado;
    }
}
