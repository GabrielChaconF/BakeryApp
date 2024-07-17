﻿

document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodasLasUnidadesDeMedida();
});


function ObtenerTodasLasUnidadesDeMedida() {
    fetch("/Ingrediente/ObtenerTodasLasUnidadesDeMedida", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelect(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarSelect(respuesta) {
    const select = document.getElementById("unidadMedida");

    respuesta.arregloUnidadesMedida.forEach(unidadMedida => {
        const option = document.createElement("option");
        option.value = unidadMedida.idUnidad;
        option.textContent = unidadMedida.nombreUnidad;
        select.appendChild(option);
    });
}


function GuardarIngrediente(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreIngrediente").value;

    const detalle = document.getElementById("detalleIngrediente").value;

    const selectUnidadMedida = document.getElementById("unidadMedida");

    const unidadMedida = selectUnidadMedida.value;



    const cantidad = document.getElementById("cantidadIngrediente").value;


    const precioUnitario = document.getElementById("precioIngrediente").value;

    const fechaVencimiento = document.getElementById("fechaVencimiento").value


    const ingrediente = {
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


    fetch("/Ingrediente/GuardarIngrediente", {
        method: "POST",
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
