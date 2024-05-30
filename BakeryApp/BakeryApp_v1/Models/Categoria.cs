using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string DescripcionCategoria { get; set; } = null!;

    public byte[] ImagenCategoria { get; set; } = null!;
}
