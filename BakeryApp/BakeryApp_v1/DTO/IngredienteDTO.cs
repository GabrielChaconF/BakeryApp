
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO;


public class IngredienteDTO
{
    public int IdIngrediente { get; set; }

    public string NombreIngrediente { get; set; } = null!;

    public string DescripcionIngrediente { get; set; } = null!;

    public int CantidadIngrediente { get; set; }

    public string UnidadMedidaIngrediente { get; set; } = null!;

    public decimal PrecioUnidadIngrediente { get; set; }

    public string FechaCaducidadIngrediente { get; set; }



    public static IngredienteDTO ConvertirIngredienteAIngredienteDTO(Ingrediente ingrediente)
    {

        if (ingrediente == null)
        {
            return null;
        }

        return new IngredienteDTO
        {
            IdIngrediente = ingrediente.IdIngrediente,
            NombreIngrediente = ingrediente.NombreIngrediente,
            DescripcionIngrediente = ingrediente.DescripcionIngrediente,
            CantidadIngrediente = ingrediente.CantidadIngrediente,
            UnidadMedidaIngrediente = ingrediente.UnidadMedidaIngrediente,
            PrecioUnidadIngrediente = ingrediente.PrecioUnidadIngrediente,
            FechaCaducidadIngrediente = ingrediente.FechaCaducidadIngrediente.ToString("yyyy-MM-dd")
        };
    }

}
