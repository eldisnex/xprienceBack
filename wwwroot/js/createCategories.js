const categories = {
   Entertainment: 0,
   Gastronomy: 0,
   Relax: 0,
   Ambiental: 0
};

for (var i = 0; i < localStorage.length; i++) {
   const cat = localStorage.getItem(localStorage.key(i));
   categories[cat]++;
   document.querySelector(`#${cat}`).textContent = categories[cat];
}

console.log(categories);
