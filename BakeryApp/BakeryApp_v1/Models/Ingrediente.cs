﻿using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Ingrediente
{
    public int IdIngrediente { get; set; }

    public string NombreIngrediente { get; set; } = null!;

    public string DescripcionIngrediente { get; set; } = null!;

    public decimal CantidadIngrediente { get; set; }

    public string UnidadMedidaIngrediente { get; set; } = null!;

    public decimal PrecioUnidadIngrediente { get; set; }

    public DateTime? FechaCaducidadIngrediente { get; set; }
}
