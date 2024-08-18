﻿using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int IdPedido { get; set; }

    public decimal? TotalPagar { get; set; }

    public DateTime FechaFactura { get; set; }

    public virtual ICollection<Detallefactura> Detallefacturas { get; set; } = new List<Detallefactura>();

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}