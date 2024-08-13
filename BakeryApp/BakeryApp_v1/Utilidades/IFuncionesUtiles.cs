using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public interface IFuncionesUtiles
{
    public Task<Categoria> GuardarImagenEnSistemaCategoria(Categoria objeto);

    public bool BorrarImagenGuardadaEnSistemaCategoria(Categoria categoria);

    public Task<bool> GuardarImagenEnSistemaProducto(Producto objeto);

    public bool BorrarImagenGuardadaEnSistemaProducto(Producto producto);

    public Task<bool> GuardarImagen3DEnSistemaProducto(Producto producto);

    public bool BorrarImagenGuardadaEnSistema3DProducto(Producto producto);
    public Persona EncriptarContraseña(Persona persona);

    public string GenerarGUID();
}
