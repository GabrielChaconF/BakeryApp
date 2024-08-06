using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryApp_v1.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public decimal PrecioProducto { get; set; }

    public int IdCategoria { get; set; }

    public int IdReceta { get; set; }

    public string ImagenProducto { get; set; } = null!;

    public string? Imagen3Dproducto { get; set; }

    public virtual ICollection<Carritocompra> Carritocompras { get; set; } = new List<Carritocompra>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Receta IdRecetaNavigation { get; set; } = null!;

    public virtual ICollection<Productosmodificado> Productosmodificados { get; set; } = new List<Productosmodificado>();

    [NotMapped]
    public IFormFile ArchivoProducto { get; set; }

    [NotMapped]
    public IFormFile Archivo3DProducto { get; set; }
}
