﻿using BakeryApp_v1.Models;

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




    public async Task<Producto> GuardarImagenEnSistemaProducto(Producto producto)
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
            return null;
        }



        return producto;
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
}