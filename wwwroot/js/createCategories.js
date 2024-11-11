const categories = {
   Entertainment: 0,
   Gastronomy: 0,
   Relax: 0,
   Ambiental: 0
};

for (var i = 0; i < localStorage.length; i++) {
   const rawCat = localStorage.getItem(localStorage.key(i));
   const cat = rawCat.split(',')[0];
   categories[cat]++;
   document.querySelector(`#${cat}`).textContent = categories[cat];
}

console.log(categories);
