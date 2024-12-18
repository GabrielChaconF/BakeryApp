﻿


document.addEventListener("DOMContentLoaded", function () {
    ObtenerUsuarioLogueado()
});



function ObtenerUsuarioLogueado() {
    fetch("/UsuarioRegistrado/ObtenerDatosUsuarioLogueado", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        crearPerfil(respuesta.mensaje)
    }).catch(error => {
        console.error("Error", error);
    });
}


function crearPerfil(mensaje) {
    const divPrincipalUsuario = document.getElementById("divPrincipalUsuario")
    const strongNombre = document.createElement("strong")
    const iconoNombre = document.createElement("i")
    const pNombre = document.createElement("p")
    const hrNombre = document.createElement("hr")
    const strongPrimerApellido = document.createElement("strong")
    const iconoPrimerApellido = document.createElement("i")
    const pPrimerApellido = document.createElement("p")
    const hrPrimerApellido = document.createElement("hr")
    const strongSegundoApellido = document.createElement("strong")
    const iconoSegundoApellido = document.createElement("i")
    const pSegundoApellido = document.createElement("p")
    const hrSegundoApellido = document.createElement("hr")
    const strongCorreo = document.createElement("strong")
    const iconoCorreo = document.createElement("i")
    const pCorreo = document.createElement("p")
    const hrCorreo = document.createElement("hr")
    const strongTelefono = document.createElement("strong")
    const iconoTelefono = document.createElement("i")
    const pTelefono = document.createElement("p")
    const hrTelefono = document.createElement("hr")





    iconoNombre.classList.add("fas", "fa-user")
    iconoPrimerApellido.classList.add("fas", "fa-user")
    iconoSegundoApellido.classList.add("fas", "fa-user")
    iconoCorreo.classList.add("fas", "fa-user")
    iconoTelefono.classList.add("fas", "fa-user")
    pNombre.classList.add("text-muted")
    pPrimerApellido.classList.add("text-muted")
    pSegundoApellido.classList.add("text-muted")
    pCorreo.classList.add("text-muted")
    pTelefono.classList.add("text-muted")

    divPrincipalUsuario.appendChild(strongNombre)
    strongNombre.appendChild(iconoNombre);
    divPrincipalUsuario.appendChild(pNombre)
    divPrincipalUsuario.appendChild(hrNombre)
    divPrincipalUsuario.appendChild(strongPrimerApellido)
    strongPrimerApellido.appendChild(iconoPrimerApellido);
    divPrincipalUsuario.appendChild(pPrimerApellido)
    divPrincipalUsuario.appendChild(hrPrimerApellido)
    divPrincipalUsuario.appendChild(strongSegundoApellido)
    strongSegundoApellido.appendChild(iconoSegundoApellido);
    divPrincipalUsuario.appendChild(pSegundoApellido)
    divPrincipalUsuario.appendChild(hrSegundoApellido)
    divPrincipalUsuario.appendChild(strongCorreo)
    strongCorreo.appendChild(iconoCorreo);
    divPrincipalUsuario.appendChild(pCorreo)
    divPrincipalUsuario.appendChild(hrCorreo)
    divPrincipalUsuario.appendChild(strongTelefono)
    strongTelefono.appendChild(iconoTelefono);
    divPrincipalUsuario.appendChild(pTelefono)
    divPrincipalUsuario.appendChild(hrTelefono)


    const textNombre = document.createTextNode("Nombre");
    strongNombre.appendChild(textNombre);
   

    const textPrimerApellido = document.createTextNode("Primer Apellido");
    strongPrimerApellido.appendChild(textPrimerApellido);


    const textSegundoApellido = document.createTextNode("Segundo Apellido");
    strongSegundoApellido.appendChild(textSegundoApellido);
 

    const textCorreo = document.createTextNode("Correo");
    strongCorreo.appendChild(textCorreo);
 

    const textTelefono = document.createTextNode("Teléfono");
    strongTelefono.appendChild(textTelefono);
   

    pNombre.innerText = mensaje.nombre
    pPrimerApellido.innerText = mensaje.primerApellido;
    pSegundoApellido.innerText = mensaje.segundoApellido
    pCorreo.innerText = mensaje.correo;
    pTelefono.innerText = mensaje.telefono;

    pNombre.setAttribute("id", "idNombre");
    pPrimerApellido.setAttribute("id", "idPrimerApellido");
    pSegundoApellido.setAttribute("id", "idSegundoApellido");
    pCorreo.setAttribute("id", "idCorreo");
    pTelefono.setAttribute("id", "idTelefono");
}



 //<strong><i class="fas fa-user mr-1"></i> Nombre </strong>

 //                           <p class="text-muted">
 //                               Prueba
 //                           </p>

 //                           <hr>

 //                           <strong><i class="fas fa-user mr-1"></i> Primer apellido </strong>

 //                           <p class="text-muted">Prueba</p>

 //                           <strong><i class="fas fa-user mr-1"></i> Segundo Apellido</strong>

 //                           <p class="text-muted">Prueba</p>
 //                           <hr>

 //                           <strong><i class="fas fa-user mr-1"></i> Correo </strong>

 //                           <p class="text-muted">
 //                               prueba@gmail.com
 //                           </p>

 //                           <hr>

 //                           <strong><i class="far fa-user mr-1"></i> Telefono </strong>

 //                           <p class="text-muted">+506 0000-8888</p>

 //                           <hr>
