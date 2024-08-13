using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public class FuncionesUtiles : IFuncionesUtiles
{
    private readonly IWebHostEnvironment ambiente;

    public FuncionesUtiles(IWebHostEnvironment ambiente)
    {
        this.ambiente = ambiente;
    }

    public async Task<Categoria> GuardarImagenEnSistemaCategoria(Categoria categoria)
    {
        string carpetaImagenes = ambiente.WebRootPath;
        carpetaImagenes = Path.Combine(carpetaImagenes, "img", "categorias");


        try
        {
            if (categoria.ArchivoCategoria.Length > 0)
            {

                string identificadorImagen = Guid.NewGuid().ToString() + Path.GetExtension(categoria.ArchivoCategoria.FileName);
                string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
                string rutaBaseDatos = "";
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "img", "categorias", identificadorImagen);

                using (Stream stream = File.Create(rutaImagenSistema))
                {
                    await categoria.ArchivoCategoria.CopyToAsync(stream);
                }

                categoria.ImagenCategoria = rutaBaseDatos;
            }
        }
        catch (Exception ex)
        {
            return null;
        }



        return categoria;
    }


    public bool BorrarImagenGuardadaEnSistemaCategoria(Categoria categoria)
    {
        string carpetaImagenes = ambiente.WebRootPath;

        try
        {
            string identificadorImagen = "";
            string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
            rutaImagenSistema = Path.Combine(rutaImagenSistema, categoria.ImagenCategoria);
            File.Delete(rutaImagenSistema);
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }




    public async Task<bool> GuardarImagenEnSistemaProducto(Producto producto)
    {
        string carpetaImagenes = ambiente.WebRootPath;
        carpetaImagenes = Path.Combine(carpetaImagenes, "img", "productos");


        try
        {
            if (producto.ArchivoProducto.Length > 0)
            {

                string identificadorImagen = Guid.NewGuid().ToString() + Path.GetExtension(producto.ArchivoProducto.FileName);
                string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
                string rutaBaseDatos = "";
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "img", "productos", identificadorImagen);

                using (Stream stream = File.Create(rutaImagenSistema))
                {
                    await producto.ArchivoProducto.CopyToAsync(stream);
                }

                producto.ImagenProducto = rutaBaseDatos;
            }
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }



    public async Task<bool> GuardarImagen3DEnSistemaProducto(Producto producto)
    {
        string carpetaImagenes = ambiente.WebRootPath;
        carpetaImagenes = Path.Combine(carpetaImagenes, "img", "imagenes3DProductos");



        try
        {

            if (producto.Archivo3DProducto is not null)
            {
                if (producto.Archivo3DProducto.Length > 0)
                {

                    string identificadorImagen = Guid.NewGuid().ToString() + Path.GetExtension(producto.Archivo3DProducto.FileName);
                    string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
                    string rutaBaseDatos = "";
                    rutaBaseDatos = Path.Combine(rutaBaseDatos, "img", "imagenes3DProductos", identificadorImagen);

                    using (Stream stream = File.Create(rutaImagenSistema))
                    {
                        await producto.Archivo3DProducto.CopyToAsync(stream);
                    }

                    producto.Imagen3Dproducto = rutaBaseDatos;
                }
                return true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }



    public bool BorrarImagenGuardadaEnSistemaProducto(Producto producto)
    {
        string carpetaImagenes = ambiente.WebRootPath;

        try
        {
            string identificadorImagen = "";
            string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
            rutaImagenSistema = Path.Combine(rutaImagenSistema, producto.ImagenProducto);
            File.Delete(rutaImagenSistema);
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }



    public bool BorrarImagenGuardadaEnSistema3DProducto(Producto producto)
    {
        string carpetaImagenes = ambiente.WebRootPath;

        try
        {
            if (!string.IsNullOrEmpty(producto.Imagen3Dproducto))
            {
                string identificadorImagen = "";
                string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
                rutaImagenSistema = Path.Combine(rutaImagenSistema, producto.Imagen3Dproducto);
                File.Delete(rutaImagenSistema);
                return true;
            }

        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }




    public Persona EncriptarContraseña(Persona persona)
    {
        try
        {
            persona.Contra = BCrypt.Net.BCrypt.HashPassword(persona.Contra);
            return persona;
        }
        catch (Exception ex)
        {

            return null;
        }


    }

    public string GenerarGUID()
    {

        return Guid.NewGuid().ToString("N");
    }
}
