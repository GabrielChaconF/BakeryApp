﻿using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
namespace BakeryApp_v1.DAO;

public interface RecetaDAO
{
    public Task Guardar(Receta receta);

    public Task Editar(Receta receta);


    public Task Eliminar(Receta receta);

    public Task<Receta> ObtenerRecetaPorId(int idReceta);
    public Task<IEnumerable<RecetaDTO>> ObtenerTodasLasRecetas(int pagina);



    public Task<Receta> ObtenerRecetaPorNombre(Receta receta);
  
    public Task<int> ContarTotalRecetas();

}
