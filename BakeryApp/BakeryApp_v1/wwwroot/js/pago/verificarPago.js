


document.addEventListener("DOMContentLoaded", function () {
    CargarAlInicio()
});


function CargarAlInicio() {
    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const checkout = parametrosUrl.get("checkout");

    if (checkout === null) {
        return 
    } else {
        VerificarPago(checkout) 
    }
}

function VerificarPago(checkout) {
    fetch("/Pedido/VerificarEstadoPago/" + checkout, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    })
    .catch(error => {
        console.error("Error", error);
    });
}
