﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryApp_v1.Models;

public partial class Pagossinpe
{
    public int IdPagoSinpe { get; set; }

    public int IdPedido { get; set; }

    public string RutaImagenSinpe { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    [NotMapped]
    public IFormFile ArchivoSinpe { get; set; }
}
