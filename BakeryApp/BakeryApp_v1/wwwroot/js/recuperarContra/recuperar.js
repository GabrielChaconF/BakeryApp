﻿





function EnviarCorreo(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const correo = document.getElementById("correo").value;

    const botonSubmit = document.getElementById("botonSubmit");

    botonSubmit.disabled = true;

    const persona = {
        Correo: correo
    }

    fetch("/Home/EnviarCorreoContra", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(persona)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    window.location.href = respuesta.mensaje
                }
                swal({
                    text: respuesta.mensaje
                });
            })
    }).catch(error => {
        console.error("Error", error);
    }).finally(() => {
        botonSubmit.disabled = false;
    });
}

function formatoTelefono(input) {

    var telefono = input.value.replace(/\-/g, '');

    if (telefono.length > 4) {
        telefono = telefono.substring(0, 4) + '-' + telefono.substring(4);
    }

    if (telefono.length > 9) {
        telefono = telefono.substring(0,8);
    }

    input.value = telefono;
}