let boton
boton = document.getElementById("BotonOscuro")

boton.addEventListener("click", ()=>{

    console.log("entre")
    document.documentElement.style.setProperty('--color1', '#000000');
    elementos.forEach(elemento => {
        
        if (getComputedStyle(elemento).color === 'rgb(0, 0, 0)') {
            elemento.style.color = 'white'; 
        }
    });
}) 



const elementos = document.querySelectorAll("p, h1, h2,a");


