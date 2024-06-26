using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public string PrecioProducto { get; set; } = null!;

    public int IdCategoria { get; set; }

    public int IdReceta { get; set; }

    public string ImagenProducto { get; set; } = null!;

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Receta IdRecetaNavigation { get; set; } = null!;
}
