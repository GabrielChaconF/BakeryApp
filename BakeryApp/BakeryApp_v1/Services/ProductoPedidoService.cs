using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public interface ProductoPedidoService
    {
        public Task Guardar(Pedidoproducto productoPedido);

        public Task<IEnumerable<Pedidoproducto>> ObtenerTodosLosProductosPorPedido(int idPedido);
    }
}
