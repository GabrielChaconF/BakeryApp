using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class RecetaDAOImpl : RecetaDAO
{
    private readonly BakeryAppContext dbContext;

    public RecetaDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Receta receta)
    {
        dbContext.Add(receta);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Receta receta)
    {
        dbContext.Update(receta);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Receta receta)
    {
        dbContext.Remove(receta);
        await dbContext.SaveChangesAsync();
    }



    public async Task<Receta> ObtenerRecetaPorId(int idReceta)
    {

        Receta RecetaEncontrado = await dbContext.Recetas.FirstOrDefaultAsync(Receta => Receta.IdReceta == idReceta);

        return RecetaEncontrado;
    }


    public async Task<IEnumerable<RecetaDTO>> ObtenerTodasLasRecetas(int pagina)
    {
        int numeroDeElementosPorPagina = 10;


        IPagedList<RecetaDTO> todasLasRecetas = dbContext.Recetas
       .Include(receta => receta.IdIngredientes)
       .OrderBy(receta => receta.IdReceta)
       .Select(receta => new RecetaDTO
       {
           IdReceta = receta.IdReceta,
           NombreReceta = receta.NombreReceta,
           Instrucciones = receta.Instrucciones,
           Ingredientes = receta.IdIngredientes.Select(ingrediente => new IngredienteDTO
           {
               IdIngrediente = ingrediente.IdIngrediente,
               NombreIngrediente = ingrediente.NombreIngrediente
           }).ToList()

       }).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);

        return todasLasRecetas;
    }

    public async Task<Receta> ObtenerRecetaPorNombre(Receta receta)
    {
        Receta RecetaEncontrada = await dbContext.Recetas.FirstOrDefaultAsync(Receta => Receta.NombreReceta == receta.NombreReceta);
        return RecetaEncontrada;
    }

 

    public async Task<int> ContarTotalRecetas()
    {
        int totalRecetas = await dbContext.Recetas.CountAsync();
        return totalRecetas;
    }


}
