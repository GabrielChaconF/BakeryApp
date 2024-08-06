using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Productosmodificado
{
    public int IdProductoModificado { get; set; }

    public int IdProductoOriginal { get; set; }

    public string Imagen3DproductoModificado { get; set; } = null!;

    public virtual ICollection<Carritocompra> Carritocompras { get; set; } = new List<Carritocompra>();

    public virtual Producto IdProductoOriginalNavigation { get; set; } = null!;
}
