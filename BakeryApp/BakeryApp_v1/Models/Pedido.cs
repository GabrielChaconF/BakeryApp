using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int IdEstadoPedido { get; set; }

    public int IdCarrito { get; set; }

    public int IdTipoEnvio { get; set; }

    public int? IdDireccion { get; set; }

    public DateTime FechaPedido { get; set; }

    public virtual Carritocompra IdCarritoNavigation { get; set; } = null!;

    public virtual Direccionesusuario? IdDireccionNavigation { get; set; }

    public virtual Estadospedido IdEstadoPedidoNavigation { get; set; } = null!;

    public virtual Tiposenvio IdTipoEnvioNavigation { get; set; } = null!;
}
