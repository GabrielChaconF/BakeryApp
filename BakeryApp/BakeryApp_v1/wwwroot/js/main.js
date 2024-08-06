

import { OrbitControls } from './controles/OrbitControls.js';
import * as THREE from './three.module.min.js';
import { FBXLoader } from './loaders/FBXLoader.js';


let camara;
let escena;
let renderer;
let luz;
let controles;
let loader;




function Iniciar(rutaImagen) {


    escena = new THREE.Scene();
    camara = new THREE.OrthographicCamera(window.innerWidth / - 2, window.innerWidth / 2, window.innerHeight / 2, window.innerHeight / - 2, - 2000, 1000);

    renderer = new THREE.WebGLRenderer({ canvas: document.getElementById("canvas") });

    renderer.setSize(window.innerWidth, window.innerHeight);

    controles = new OrbitControls(camara, document.getElementById("canvas"));


    camara.position.set(0, 50, 50);
    loader = new FBXLoader();



    //Depende del objeto que se cargue necesita luz de fondo, para que no se vea completamente negro

    luz = new THREE.DirectionalLight(0xacb5ae, 1)
    escena.add(luz);

    loader.load(rutaImagen, (objeto) => {
        objeto;

        objeto.scale.set(1, 1, 1);
        objeto.position.y = 0;
        objeto.position.z = 0;
        objeto.position.y = 0;

        escena.add(objeto);


    });





    Animar()
}


function VerificarCamaraYNegativa() {
    if (camara.position.y < 0) {
        return true;
    }
}





function Animar() {
    requestAnimationFrame(Animar);

    if (VerificarCamaraYNegativa()) {
        camara.position.y = 100;
    }
    // Se copia la posicion de la luz a la camara cada frame
    luz.position.copy(camara.position);
    controles.update()

    renderer.render(escena, camara);
}



export { Iniciar}