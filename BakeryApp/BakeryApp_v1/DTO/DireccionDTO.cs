namespace BakeryApp_v1.DTO
{
    public class DireccionDTO
    {
        public int IdDireccion { get; set; }
        public string NombreDireccion { get; set; } = null!;

        public string DireccionExacta { get; set; } = null!;

        public ProvinciaDTO ProvinciaDTO { get; set; } = null!;

        public CantonDTO CantonDTO { get; set; } = null!;

        public DistritoDTO DistritoDTO { get; set; } = null!;


      
    }
}
