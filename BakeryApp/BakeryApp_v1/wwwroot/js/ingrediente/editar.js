


document.addEventListener("DOMContentLoaded", function () {
    ObtenerIngredienteEspecifico();
});



function ObtenerIngredienteEspecifico() {


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    idIngrediente = parametrosUrl.get("idIngrediente");


    fetch("/Ingrediente/DevolverIngredienteEspecifico/" + idIngrediente, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        RellenarDatosFormulario(respuesta.ingrediente)
    }).catch(error => {
        console.error("Error", error);
    });
}



function EditarIngrediente(event) {
    event.preventDefault();


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreIngrediente").value;

    const detalle = document.getElementById("detalleIngrediente").value;

    const unidadMedida = document.getElementById("unidadMedida").value;

    const cantidad = document.getElementById("cantidadIngrediente").value;


    const precioUnitario = document.getElementById("precioIngrediente").value;

    const fechaVencimiento = document.getElementById("fechaVencimiento").value





    const ingrediente = {
        idIngrediente: parametrosUrl.get("idIngrediente"),
        NombreIngrediente: nombre,
        DescripcionIngrediente: detalle,
        CantidadIngrediente: cantidad,
        UnidadMedidaIngrediente: unidadMedida,
        PrecioUnidadIngrediente: precioUnitario,
        FechaCaducidadIngrediente: fechaVencimiento
    }

    if (VerificarCantidadVacia(cantidad)) {
        swal({
            text: "El campo de cantidad no puede estar vacio"
        });
        return;
    }

    if (VerificarPrecioVacio(precioUnitario)) {
        swal({
            text: "El campo de precio unitario no puede estar vacio"
        });
        return;
    }


    if (VerificarFechaVacia()) {
        swal({
            text: "La fecha no puede estar vacia"
        });
        return;
    }


    fetch("/Ingrediente/GuardarEditado", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(ingrediente)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                swal({
                    text: respuesta.mensaje
                });
            })
    }).catch(error => {
        console.error("Error", error);
    });
}


function RellenarDatosFormulario(ingrediente) {

    const nombre = document.getElementById("nombreIngrediente");

    const descripcion = document.getElementById("detalleIngrediente");

    const cantidad = document.getElementById("cantidadIngrediente");

    const unidadMedida = document.getElementById("unidadMedida");

    const precio = document.getElementById("precioIngrediente")

    const fechaVencimiento = document.getElementById("fechaVencimiento")

    nombre.value = ingrediente.nombreIngrediente;

    descripcion.value = ingrediente.descripcionIngrediente;

    cantidad.value = ingrediente.cantidadIngrediente;

    unidadMedida.value = ingrediente.unidadMedidaIngrediente;

    precio.value = ingrediente.precioUnidadIngrediente;

    fechaVencimiento.value = ingrediente.fechaCaducidadIngrediente;


}


function VerificarFechaVacia() {

    const fechaVencimiento = document.getElementById("fechaVencimiento").value


    if (!fechaVencimiento) {
        return true;
    }

    return false;
}

function VerificarCantidadVacia(cantidad) {

    if (cantidad === "") {

        return true;
    }

    return false;
}

function VerificarPrecioVacio(precio) {

    if (precio === "") {

        return true;
    }

    return false;
}
