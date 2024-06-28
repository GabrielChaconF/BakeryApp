namespace BakeryApp_v1.DTO;

public class RecetaDTO
{
    public int IdReceta { get; set; }

    public string NombreReceta { get; set; } = null!;

    public string Instrucciones { get; set; } = null!;


    public List<IngredienteDTO> Ingredientes { get; set; } = new List<IngredienteDTO>();




}
