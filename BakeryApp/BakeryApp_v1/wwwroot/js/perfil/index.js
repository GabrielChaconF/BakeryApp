


document.addEventListener("DOMContentLoaded", function () {
    CargarAlInicio();
});



function CargarAlInicio() {
    ObtenerTodasLasPaginas()

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodosLasPersonas(1)
    } else {
        ObtenerTodasLasCategorias(pagina)
    }


}



function ObtenerTodosLasPersonas(pagina) {
    fetch("/Perfil/ObtenerTodasLasPersonas/" + pagina, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).catch(error => {
        console.error("Error", error);
    });
}

function ObtenerTodasLasPaginas() {



    fetch("/Perfil/ObtenerTotalPaginas", {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearPaginacion(respuesta.paginas)
    }).catch(error => {
        console.error("Error", error);
    });
}

function CrearPaginacion(paginas) {
    const lista = document.createElement("ul");

    const tabla = document.getElementById("tabla")

    lista.classList.add("pagination", "justify-content-center", "m-3")



    var contador = 1;
    while (contador <= paginas) {
        var elementoLista = document.createElement("li")
        var aLista = document.createElement("a")

        elementoLista.classList.add("page-item")
        aLista.classList.add("page-link", "bg-dark")


        lista.appendChild(elementoLista);
        elementoLista.appendChild(aLista)


        aLista.setAttribute("pagina", contador);
        aLista.href = "?pagina=" + contador;
        aLista.setAttribute("pagina", contador);

        aLista.addEventListener("click", function (event) {
            ObtenerTodasLasCategorias(event.target.getAttribute("pagina"));
        });

        aLista.innerText = contador;
        contador++;
    }

    tabla.appendChild(lista);

}

function CrearFilasTabla(respuesta) {
  
}


//<tr>
//    <td>1</td>
//    <td>
//        Prueba
//    </td>
//    <td>Efectivo</td>
//    <td> 3</td>
//    <td>₡5000.00</td>
//    <td>₡5000.00</td>
//    <td>
//        <div class="text-center">
//            <a href="@Url.Action(" VerFactura", "Administrador")" class="btn btn-sm btn-primary">
//            Ver
//        </a>
//        <a href="#" class="btn btn-sm ">
//            Eliminar
//        </a>
//    </div>
//    </td>  
//</tr > 