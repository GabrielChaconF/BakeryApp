



document.addEventListener('DOMContentLoaded', () => {
    ObtenerDireccionesUsuario()
   

    IniciarEventListeners()

    IniciarTabPane()

    ObtenerTodasLasProvincias();

    ObtenerTodosLosPedidosPorCliente()
});


function ObtenerTodosLosPedidosPorCliente() {
    fetch("/Pedido/ObtenerPedidosUsuarioLogueado", {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearFilasTabla(respuesta)
    }).catch(error => {

        console.error("Error", error);
    });
}

function CrearFilasTabla(respuesta) {
    var contador = 1;
    const bodyTabla = document.getElementById("agregar");
    bodyTabla.innerHTML = "";

    if (respuesta.arregloPedidos.length == 0) {
        bodyTabla.innerText = "El usuario no tiene pedidos realizados";
    }

    respuesta.arregloPedidos.forEach(pedido => {
        const fila = document.createElement("tr");

        const tdId = document.createElement("td");
        const tdFecha = document.createElement("td");
        const tdEstado = document.createElement("td");
        const tdVer = document.createElement("td");
        const tdCancelarEstado = document.createElement("td");
        const tdModificarSinpe = document.createElement("td");
        const tdVerFactura = document.createElement("td");

        const aVer = document.createElement("a");
        const formCancelar = document.createElement("form");
        const botonCancelar = document.createElement("button");
        const aModificarSinpe = document.createElement("a");
        const aVerFactura = document.createElement("a");


        tdId.classList.add("text-center", "align-middle");
        tdFecha.classList.add("text-center", "align-middle");
        tdEstado.classList.add("text-center", "align-middle");
        tdVer.classList.add("text-center", "align-middle");
        tdCancelarEstado.classList.add("text-center", "align-middle");
        tdModificarSinpe.classList.add("text-center", "align-middle");

        aVer.classList.add("btn", "btn-primary", "btn-sm", "text-white");
        botonCancelar.classList.add("btn", "btn-danger", "btn-sm", "text-white");
        aModificarSinpe.classList.add("btn", "btn-danger", "btn-sm", "text-white");
        aVerFactura.classList.add("btn", "btn-danger", "btn-sm", "text-white");
   
        botonCancelar.innerText = "Cancelar Pedido";
        tdId.innerText = "Pedido: " + contador;
        tdFecha.innerText = pedido.fechaPedido;
        tdEstado.innerText = pedido.estadoPedido.nombreEstado;
        aVer.innerText = "Ver Pedido";
        aVerFactura.innerText = "Ver Factura del pedido"

        tdVerFactura.appendChild(aVerFactura)
        tdVer.appendChild(aVer);
        formCancelar.appendChild(botonCancelar);
        tdCancelarEstado.appendChild(formCancelar);

        fila.appendChild(tdId);
        fila.appendChild(tdFecha);
        fila.appendChild(tdEstado);
        fila.appendChild(tdVer);
        fila.appendChild(tdCancelarEstado);
        fila.appendChild(tdModificarSinpe);
        fila.appendChild(tdVerFactura);  
 
        if (pedido.tipoPago.idTipoPago == 2) {
            aModificarSinpe.innerText = "Modificar Imagen del Sinpe";
            tdModificarSinpe.appendChild(aModificarSinpe);
        }


        aVerFactura.setAttribute("idPedido", pedido.idPedido);
        aVerFactura.addEventListener("click", (event) => {
            VerPaginaFactura(event);
        });

        bodyTabla.appendChild(fila);

        contador++;

        botonCancelar.setAttribute("idPedido", pedido.idPedido);
        botonCancelar.addEventListener("click", (event) => {
            CancelarPedido(event);
        });


        aModificarSinpe.setAttribute("idPedido", pedido.idPedido);
        aModificarSinpe.addEventListener("click", (event) => {
            VerPaginaModificarSinpe(event);
        });

        aVer.setAttribute("idPedido", pedido.idPedido);
        aVer.addEventListener("click", (event) => {
            VerPaginaPedido(event);
        });
    });
}




function CancelarPedido(event) {
    event.preventDefault()
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const botonCancelar = event.currentTarget;
    const idPedido = botonCancelar.getAttribute("idPedido");
  


    const pedido = {
        IdPedido: idPedido,
    }


    fetch("/Pedido/CancelarPedido", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(pedido)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ObtenerTodosLosPedidosPorCliente() 
                } else {
                    swal({
                        text: respuesta.mensaje,
                        icon: "error"
                    });
                }

            })
    }).catch(error => {
        console.error("Error", error);
    });
}


function VerPaginaFactura(event) {
    const idPedido = event.currentTarget.getAttribute("idPedido");



    var urlVer = "/Pedido/VerFactura?idPedido=" + idPedido;


    window.location.replace(urlVer);
}



function VerPaginaPedido(event) {
    const idPedido = event.currentTarget.getAttribute("idPedido");



    var urlVer = "/Pedido/VerPedido?idPedido=" + idPedido;


    window.location.replace(urlVer);
}


function VerPaginaModificarSinpe(event) {
    const idPedido = event.currentTarget.getAttribute("idPedido");



    var urlVer = "/Pedido/ModificarSinpe?idPedido=" + idPedido;


    window.location.replace(urlVer);
}


function IniciarTabPane() {

    const contenedorPaneles = document.getElementById("contenerPaneles")
    const links = contenedorPaneles.querySelectorAll('.nav-link');

    links.forEach(link => {
        link.addEventListener("click", (event) => {

            event.preventDefault();

            links.forEach(link => {
                link.classList.remove('active')
            });


            const panelActual = document.querySelector(link.getAttribute('href'));
            document.querySelectorAll('.tab-pane').forEach(pane => {
                pane.classList.remove('active')
            });

            link.classList.add('active');
            panelActual.classList.add('active');


            FiltrarContenidoPorPanel(panelActual.id);
        });
    });

    const tabPaneActivo = document.querySelector('.tab-pane.active');
    if (tabPaneActivo) {
        FiltrarContenidoPorTabPane(tabPaneActivo.id);
    }
}

function IniciarEventListeners() {
    const selectProvincia = document.getElementById("provinciaAgregar");
    const selectCanton = document.getElementById("cantonAgregar");
    const selectProvinciaModificar = document.getElementById("provinciaModificar");
    const selectCantonModificar = document.getElementById("cantonModificar")


    selectProvinciaModificar.addEventListener("change", () => {
        ObtenerCantonesModificar();
        ObtenerDistritosModificar();
    });

    selectCantonModificar.addEventListener("change", () => {
        ObtenerDistritosModificar();
    });


    selectProvincia.addEventListener("change", () => {
        ObtenerCantones();
        ObtenerDistritos();
    });

    selectCanton.addEventListener("change", () => {
        ObtenerDistritos();
    });

}


function FiltrarContenidoPorPanel(idPanel) {
    const formEditarCuenta = document.getElementById("formEditarCuenta");
    const formEliminarCuenta = document.getElementById("formEliminarCuenta");
    const formAgregarDireccion = document.getElementById("formAgregarDireccion");
    const formModificarDireccion = document.getElementById("formModificarDireccion");
    const formEliminarDireccion = document.getElementById("formEliminarDireccion");
 




    switch (idPanel) {
        case "verpedidos":
            // Aqui de igual manera no se ocupa llamar a ver pedidos ya que se llama apenas se carga el DOM
            break;
        case "editarcuenta":
            if (formEditarCuenta) {
                formEditarCuenta.removeEventListener("submit", EditarPersona);
                formEditarCuenta.addEventListener("submit", EditarPersona);
            }
            break;
        case "eliminarcuenta":
            if (formEliminarCuenta) {
                formEliminarCuenta.removeEventListener("submit", EliminarPersona);
                formEliminarCuenta.addEventListener("submit", EliminarPersona);
            }
            break;
        case "agregardireccion":
            if (formAgregarDireccion) {
                formAgregarDireccion.removeEventListener("submit", AgregarDireccion);
                formAgregarDireccion.addEventListener("submit", AgregarDireccion);
            }
         
            break;
        case "modificardireccion":
            if (formModificarDireccion) {
                formModificarDireccion.removeEventListener("submit", ModificarDireccion);
                formModificarDireccion.addEventListener("submit", ModificarDireccion);
            }
           
            break;
        case "eliminardireccion":
            if (formEliminarDireccion) {
                formEliminarDireccion.removeEventListener("submit", EliminarDireccionUsuario);
                formEliminarDireccion.addEventListener("submit", EliminarDireccionUsuario);
            }
            break;
        case "verdirecciones":
            // Aqui no se ocupa llamar a  ObtenerDireccionesUsuario(), ya que se llama apenas se carga el DOM
            break;
        default:
            swal({
                text: "Opción Invalida",
                icon: "error"
            });
    }
}




function ObtenerDireccionesUsuario() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDirecciones(respuesta.arregloDirecciones)
        LlenarSelectEliminarDireccion(respuesta.arregloDirecciones)
        LlenarSelectModificarDireccion(respuesta.arregloDirecciones)
    }).catch(error => {
        console.error("Error", error);
    });
}




function LlenarSelectEliminarDireccion(arregloDirecciones) {
    const select = document.getElementById("direccionEliminar");



    select.innerHTML = "";
    arregloDirecciones.forEach(direccion => {
        const option = document.createElement("option");
        option.value = direccion.idDireccion;
        option.textContent = direccion.nombreDireccion;
        select.appendChild(option);
    });

}

function LlenarSelectModificarDireccion(arregloDirecciones) {
    const select = document.getElementById("direccionModificar");



    select.innerHTML = "";
    arregloDirecciones.forEach(direccion => {
        const option = document.createElement("option");
        option.value = direccion.idDireccion;
        option.textContent = direccion.nombreDireccion;
        select.appendChild(option);
    });

}


function LlenarTablaDirecciones(arregloDirecciones) {
    const bodyTabla = document.getElementById("tablaDirecciones")

    if (arregloDirecciones.length == 0) {
        const divTabla = document.getElementById("cardTabla")
        const divHeader = document.createElement("div")
        const h3Mensaje = document.createElement("h3")

        divHeader.classList.add("card-header")
        h3Mensaje.classList.add("card-title")

        divTabla.append(divHeader)
        divHeader.appendChild(h3Mensaje)

        h3Mensaje.innerText = "El usuario no tiene direcciones asociadas"
    }

    arregloDirecciones.forEach(direccion => {
        const filaTabla = document.createElement("tr")
        const tdNombre = document.createElement("td")
        const tdProvincia = document.createElement("td")
        const tdCanton = document.createElement("td")
        const tdDistrito = document.createElement("td")
        const tdDireccion = document.createElement("td")

        bodyTabla.appendChild(filaTabla)
        filaTabla.appendChild(tdNombre)
        filaTabla.appendChild(tdProvincia)
        filaTabla.appendChild(tdCanton)
        filaTabla.appendChild(tdDistrito)
        filaTabla.appendChild(tdDireccion)

        tdNombre.innerText = direccion.nombreDireccion
        tdProvincia.innerText = direccion.provinciaDTO.nombreProvincia
        tdCanton.innerText = direccion.cantonDTO.nombreCanton
        tdDistrito.innerText = direccion.distritoDTO.nombreDistrito
        tdDireccion.innerText = direccion.direccionExacta

    })

}


function ObtenerCantonesModificar() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id de la provincia
    const selectProvincia = document.getElementById("provinciaModificar");

    let idProvincia = selectProvincia.value;





    ObtenerTodasLosCantonesPorProvincia(idProvincia);
}


function ObtenerDistritosModificar() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id del canton
    const selectCanton = document.getElementById("cantonModificar");

    let idCanton = selectCanton.value;



    ObtenerTodasLosDistritosPorCanton(idCanton)
}





function ObtenerCantones() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id de la provincia
    const selectProvincia = document.getElementById("provinciaAgregar");

    let idProvincia = selectProvincia.value;





    ObtenerTodasLosCantonesPorProvincia(idProvincia);
}


function ObtenerDistritos() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id del canton
    const selectCanton = document.getElementById("cantonAgregar");

    let idCanton = selectCanton.value;



    ObtenerTodasLosDistritosPorCanton(idCanton)
}



function ObtenerTodasLasProvincias() {
    fetch("/UsuarioRegistrado/ObtenerTodasProvincias", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectProvincias(respuesta)
        LlenarSelectProvinciasModificar(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function ObtenerTodasLosCantonesPorProvincia(idProvincia) {



    fetch("/UsuarioRegistrado/ObtenerTodasLosCantonesPorProvincia/" + idProvincia, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectCantones(respuesta)
        LlenarSelectCantonesModificar(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function ObtenerTodasLosDistritosPorCanton(idCanton) {

    fetch("/UsuarioRegistrado/ObtenerTodasLosDistritosPorCanton/" + idCanton, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectDistritos(respuesta)
        LlenarSelectDistritosModificar(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarSelectProvincias(respuesta) {
    const select = document.getElementById("provinciaAgregar");

    select.innerHTML = "";
    respuesta.arregloProvincias.forEach(provincia => {
        const option = document.createElement("option");
        option.value = provincia.idProvincia;
        option.textContent = provincia.nombreProvincia;
        select.appendChild(option);
    });

    // Se establece como seleccionado el primer elemento del select de provincias
    select.selectedIndex = 0;
    ObtenerCantones();



}

function LlenarSelectProvinciasModificar(respuesta) {
    const select = document.getElementById("provinciaModificar");

    select.innerHTML = "";
    respuesta.arregloProvincias.forEach(provincia => {
        const option = document.createElement("option");
        option.value = provincia.idProvincia;
        option.textContent = provincia.nombreProvincia;
        select.appendChild(option);
    });

    // Se establece como seleccionado el primer elemento del select de provincias
    select.selectedIndex = 0;
    ObtenerCantonesModificar();



}


function LlenarSelectCantones(respuesta) {
    const select = document.getElementById("cantonAgregar");

    select.innerHTML = ""


    respuesta.arregloCantones.forEach(canton => {
        const option = document.createElement("option");
        option.value = canton.idCanton;
        option.textContent = canton.nombreCanton;
        select.appendChild(option);
    });

    select.selectedIndex = 0;
    // Se establece como seleccionado el primer elemento del select de cantones
    ObtenerDistritos();



}

function LlenarSelectCantonesModificar(respuesta) {
    const select = document.getElementById("cantonModificar");

    select.innerHTML = ""


    respuesta.arregloCantones.forEach(canton => {
        const option = document.createElement("option");
        option.value = canton.idCanton;
        option.textContent = canton.nombreCanton;
        select.appendChild(option);
    });

    select.selectedIndex = 0;
    // Se establece como seleccionado el primer elemento del select de cantones
    ObtenerDistritosModificar();



}


function LlenarSelectDistritos(respuesta) {
    const select = document.getElementById("distritoAgregar");

    select.innerHTML = ""

    respuesta.arregloDistritos.forEach(distrito => {
        const option = document.createElement("option");
        option.value = distrito.idDistrito;
        option.textContent = distrito.nombreDistrito;
        select.appendChild(option);
    });

}

function LlenarSelectDistritosModificar(respuesta) {
    const select = document.getElementById("distritoModificar");

    select.innerHTML = ""

    respuesta.arregloDistritos.forEach(distrito => {
        const option = document.createElement("option");
        option.value = distrito.idDistrito;
        option.textContent = distrito.nombreDistrito;
        select.appendChild(option);
    });

}


function EliminarPersona(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const contra = document.getElementById("contraEliminar").value;

    const persona = {
        Contra: contra
    }

    fetch("/UsuarioRegistrado/EliminarUsuario", {
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
    });
}


function EliminarDireccionUsuario(event) {
    event.preventDefault()

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const selectEliminarDireccion = document.getElementById("direccionEliminar");

    const idDireccion = selectEliminarDireccion.value;


    if (idDireccion == "") {
        swal({
            text: "No puede eliminar una direccion, ya que no hay direcciones registradas",
            icon: "error"
        })
    }

    fetch("/UsuarioRegistrado/EliminarDireccion/" + idDireccion, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        }
    }).then(respuesta => respuesta.json())
        .then(respuesta => {
            if (respuesta.correcto) {
                swal({
                    text: respuesta.mensaje,
                    icon: "success"
                });
                ActualizarInterfazDireccionEliminada(idDireccion)

            } else {
                swal({
                    text: respuesta.mensaje,
                    icon: "error"
                });
            }

        }).catch(error => {
            console.error("Error", error);
        });
}


function ActualizarInterfazDireccionEliminada(idDireccion) {
    const selectEliminarDireccion = document.getElementById("direccionEliminar");



    for (let i = 0; i < selectEliminarDireccion.options.length; i++) {
        if (selectEliminarDireccion.options[i].value == idDireccion) {
            selectEliminarDireccion.remove(i);
            break;
        }
    }
    ActualizarInterfazDireccionEliminadaTabla()
}





function ModificarDireccion(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const selectModificarDireccion = document.getElementById("direccionModificar");
    const idDireccion = selectModificarDireccion.value;
    const nombreDireccion = document.getElementById("nombreDireccionModificar").value;
    const direccionExacta = document.getElementById("direccionExactaModificar").value;
    const selectProvincia = document.getElementById("provinciaModificar");
    const selectCanton = document.getElementById("cantonModificar");
    const selectDistrito = document.getElementById("distritoModificar")

    const idProvincia = selectProvincia.value;
    const idCanton = selectCanton.value;
    const idDistrito = selectDistrito.value;

    



    const direccion = {
        IdDireccion: idDireccion,
        NombreDireccion: nombreDireccion,
        DireccionExacta: direccionExacta,
        IdProvincia: idProvincia,
        idCanton: idCanton,
        IdDistrito: idDistrito
    }

    fetch("/UsuarioRegistrado/ModificarDireccion", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(direccion)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ActualizarInterfazDireccionModificada(idDireccion, nombreDireccion)
                   
                } else {
                    swal({
                        text: respuesta.mensaje,
                        icon: "error"
                    });
                }
               
            })
    }).catch(error => {
        console.error("Error", error);
    });

}


function ActualizarInterfazDireccionModificada(idDireccion, nombreDireccion) {
    const selectModificarDireccion = document.getElementById("direccionModificar");



    for (let i = 0; i < selectModificarDireccion.options.length; i++) {
        if (selectModificarDireccion.options[i].value == idDireccion) {
            selectModificarDireccion.options[i].textContent = nombreDireccion;
            break;
        }
    }

    const selectEliminarDireccion = document.getElementById("direccionEliminar");



    for (let i = 0; i < selectEliminarDireccion.options.length; i++) {
        if (selectEliminarDireccion.options[i].value == idDireccion) {
            selectEliminarDireccion.options[i].textContent = nombreDireccion
            break;
        }
    }


    ActualizarInterfazDireccionModificadaTabla()
}




function AgregarDireccion(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;


    const nombreDireccion = document.getElementById("nombreDireccionAgregar").value;
    const direccionExacta = document.getElementById("direccionExactaAgregar").value;
    const selectProvincia = document.getElementById("provinciaAgregar");
    const selectCanton = document.getElementById("cantonAgregar");
    const selectDistrito = document.getElementById("distritoAgregar")

    const idProvincia = selectProvincia.value;
    const idCanton = selectCanton.value;
    const idDistrito = selectDistrito.value;




    const direccion = {
        NombreDireccion: nombreDireccion,
        DireccionExacta: direccionExacta,
        IdProvincia: idProvincia,
        idCanton: idCanton,
        IdDistrito: idDistrito
    }

    fetch("/UsuarioRegistrado/AgregarDireccion", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(direccion)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ActualizarInterfazDireccionAgregada();
                } else {
                    swal({
                        text: respuesta.mensaje,
                        icon: "error"
                    });
                }
               
            })
    }).catch(error => {
        console.error("Error", error);
    });

}



function EditarPersona(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombre").value;

    const primerApellido = document.getElementById("primerApellido").value;

    const segundoApellido = document.getElementById("segundoApellido").value;

    const telefono = document.getElementById("telefono").value;

    const persona = {
        Nombre: nombre,
        PrimerApellido: primerApellido,
        SegundoApellido: segundoApellido,
        Telefono: telefono
    }

    fetch("/UsuarioRegistrado/EditarUsuario", {
        method: "PUT",
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
                  
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ActualizarInterfazUsuarioModificado()
                } else {
                    swal({
                        text: respuesta.mensaje,
                        icon: "error"
                    });
                }
            })
    }).catch(error => {
        console.error("Error", error);
    });
}


function ActualizarInterfazUsuarioModificado() {
    fetch("/UsuarioRegistrado/ObtenerDatosUsuarioLogueado", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        
        ActualizarDatosUsuariosModificados(respuesta.mensaje)
    }).catch(error => {
        console.error("Error", error);
    });
}

function ActualizarDatosUsuariosModificados(mensaje) {
    const nombre = document.getElementById("idNombre")

    const primerApellido = document.getElementById("idPrimerApellido")

    const segundoApellido = document.getElementById("idSegundoApellido")

    const correo = document.getElementById("idCorreo")

    const telefono = document.getElementById("idTelefono")

    nombre.innerText = mensaje.nombre

    primerApellido.innerText = mensaje.primerApellido

    segundoApellido.innerText = mensaje.segundoApellido

    correo.innerText = mensaje.correo;

    telefono.innerText = mensaje.telefono

}

function ActualizarInterfazDireccionAgregada() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDireccionesInterfaz(respuesta.arregloDirecciones)
        LlenarSelectEliminarDireccion(respuesta.arregloDirecciones)
        LlenarSelectModificarDireccion(respuesta.arregloDirecciones)
    }).catch(error => {
        console.error("Error", error);
    });
}




function ActualizarInterfazDireccionEliminadaTabla() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDireccionesInterfaz(respuesta.arregloDirecciones)
    }).catch(error => {
        console.error("Error", error);
    });
}


function ActualizarInterfazDireccionModificadaTabla() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDireccionesInterfaz(respuesta.arregloDirecciones)
    }).catch(error => {
        console.error("Error", error);
    });
}




function LlenarTablaDireccionesInterfaz(arregloDirecciones) {
    const bodyTabla = document.getElementById("tablaDirecciones")

    //Se limpia la tabla
    bodyTabla.innerHTML = "";

    arregloDirecciones.forEach(direccion => {
        const filaTabla = document.createElement("tr")
        const tdNombre = document.createElement("td")
        const tdProvincia = document.createElement("td")
        const tdCanton = document.createElement("td")
        const tdDistrito = document.createElement("td")
        const tdDireccion = document.createElement("td")

        bodyTabla.appendChild(filaTabla)
        filaTabla.appendChild(tdNombre)
        filaTabla.appendChild(tdProvincia)
        filaTabla.appendChild(tdCanton)
        filaTabla.appendChild(tdDistrito)
        filaTabla.appendChild(tdDireccion)

        tdNombre.innerText = direccion.nombreDireccion
        tdProvincia.innerText = direccion.provinciaDTO.nombreProvincia
        tdCanton.innerText = direccion.cantonDTO.nombreCanton
        tdDistrito.innerText = direccion.distritoDTO.nombreDistrito
        tdDireccion.innerText = direccion.direccionExacta

    })

}



function formatoTelefono(input) {

    var telefono = input.value.replace(/\-/g, '');

    if (telefono.length > 4) {
        telefono = telefono.substring(0, 4) + '-' + telefono.substring(4);
    }

    if (telefono.length > 9) {
        telefono = telefono.substring(0, 8);
    }

    input.value = telefono;
}

