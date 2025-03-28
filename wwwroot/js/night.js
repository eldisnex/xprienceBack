let boton;
boton = document.getElementById("BotonOscuro");

let night = true;


boton.addEventListener("click", () => {
    console.log("entre");
    if (!night) document.documentElement.style.setProperty("--color1", "#000000");
    else document.documentElement.style.setProperty("--color1", "#f5f5f5");
    elementos.forEach((elemento) => {
      if (getComputedStyle(elemento).color === "rgb(0, 0, 0)") {
        elemento.style.color = "white";
      }
    });
  night = !night;
}); 

const elementos = document.querySelectorAll("p, h1, h2,a");
