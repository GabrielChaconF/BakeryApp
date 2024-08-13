import { Iniciar } from "../main.js"


document.addEventListener("DOMContentLoaded", function () {
    ObtenerProductoEspecifico();
});



function ObtenerProductoEspecifico() {


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const idProducto = parametrosUrl.get("idProducto")


    fetch("/ProductoEmpleado/DevolverProductoEspecifico/" + idProducto, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {

        const imagen3D = "/" + respuesta.producto.imagen3Dproducto;

        Iniciar(imagen3D)
       
       
    }).catch(error => {
        console.error("Error", error);
    });
}









