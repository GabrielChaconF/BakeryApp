using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO;

public class CategoriaDAOImpl : CategoriaDAO
{
    private readonly BakeryAppContext dbContext;

    public CategoriaDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Categoria categoria) 
    {
        dbContext.Categorias.Add(categoria);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Categoria categoria)
    {
        dbContext.Categorias.Update(categoria);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Categoria categoria)
    {
        dbContext.Categorias.Remove(categoria);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Categoria> ObtenerCategoriaEspecifica(Categoria categoria)
    {
        Categoria categoriaEncontrada = await dbContext.Categorias.FirstOrDefaultAsync(Categoria => Categoria.IdCategoria == categoria.IdCategoria);
        return categoriaEncontrada;
    }

    public async Task<Categoria> ObtenerCategoriaPorId(int idCategoria)
    {
        Categoria categoriaEncontrada = await dbContext.Categorias.FirstOrDefaultAsync(Categoria => Categoria.IdCategoria == idCategoria);
        return categoriaEncontrada;
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias()
    {
        IEnumerable<Categoria> todasLasCategorias = await dbContext.Categorias.ToListAsync();
        return todasLasCategorias;
    }

    public async Task<Categoria> ObtenerCategoriaPorNombre(Categoria categoria)
    {
        Categoria categoriaEncontrada = await dbContext.Categorias.FirstOrDefaultAsync(Categoria => Categoria.NombreCategoria == categoria.NombreCategoria);
        return categoriaEncontrada;
    }

}
