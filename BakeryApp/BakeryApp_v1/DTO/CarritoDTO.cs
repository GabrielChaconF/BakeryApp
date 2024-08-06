using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO;

public class CarritoDTO
{
    public int IdCarrito { get; set; }

    public int IdPersona { get; set; }

    public int? IdProducto { get; set; }

    public int CantidadProducto { get; set; }

    public int? IdProductoModificado { get; set; }

    public bool Estado { get; set; }

    public virtual Productosmodificado? IdProductoModificadoNavigation { get; set; }

    public virtual ProductoDTO? ProductoDTO { get; set; }
}
