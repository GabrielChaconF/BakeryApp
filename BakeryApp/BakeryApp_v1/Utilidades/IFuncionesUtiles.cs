using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public interface IFuncionesUtiles
{
    public Task<Categoria> GuardarImagenEnSistemaCategoria(Categoria objeto);

    public bool BorrarImagenGuardadaEnSistemaCategoria(Categoria categoria);


    public Persona EncriptarContraseña(Persona persona);
}
