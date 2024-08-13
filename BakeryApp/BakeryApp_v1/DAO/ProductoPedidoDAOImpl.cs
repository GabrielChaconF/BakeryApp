using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
namespace BakeryApp_v1.DAO
{
    public class ProductoPedidoDAOImpl : ProductoPedidoDAO
    {
        private readonly BakeryAppContext dbContext;

        public ProductoPedidoDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task Guardar(Pedidoproducto productoPedido)
        {
            dbContext.Add(productoPedido);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pedidoproducto>> ObtenerTodosLosProductosPorPedido(int idPedido)
        {
            IEnumerable<Pedidoproducto> todosLosProductosPorPedido = await dbContext.Pedidoproductos.Include(Producto => Producto.IdProductoNavigation).Where(ProductoPedido => ProductoPedido.IdPedido == idPedido).ToListAsync();
            return todosLosProductosPorPedido;
        }
    }
}
