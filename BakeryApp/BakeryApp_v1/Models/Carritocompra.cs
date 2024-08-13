using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Carritocompra
{
    public int IdCarrito { get; set; }

    public int IdPersona { get; set; }

    public int? IdProducto { get; set; }

    public int CantidadProducto { get; set; }

    public int? IdProductoModificado { get; set; }

    public bool Estado { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Productosmodificado? IdProductoModificadoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
