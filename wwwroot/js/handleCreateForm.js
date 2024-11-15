'use strict';

const categories = {
   Entertainment: [10000, 14000, 17000, 18000],
   Gastronomy: [13000],
   Relax: [16000],
   Ambiental: [16000]
};

$('form').submit((e) => onSent(e));

const options = {};

navigator.geolocation.getCurrentPosition((position) => {
   options.latitude = position.coords.latitude;
   options.longitude = position.coords.longitude;
});

if (!options.latitude) {
   options.latitude = -34.6019627;
}
if (!options.longitude) {
   options.longitude = -58.4286208;
}
const onSent = (e) => {
   e.preventDefault();
   document
      .querySelectorAll('.cardContainerFlex > div')
      .forEach((e) => e.remove());
   $('#closeBlock').prop('checked', true);
   console.log(e.target);
   let formdata = new FormData(e.target);
   console.log(formdata);
   let data = {};
   [...formdata.keys()].forEach((key) => {
      let values = formdata.getAll(key);
      data[key] = values.length > 1 ? values : values.join();
   });
   Object.assign(options, data);
   options.code = categories[categorie].join(',');
   $.post(handleCreateUrl, options)
      .then((res) => JSON.parse(res).results)
      .then((res) => {
         res.forEach((item) => {
            createLine(item);
         });
      });
};

const createLine = (item) => {
   const template = document.getElementById('subCardTemplate').content;
   const clone = document.importNode(template, true);
   $.post(handleImage, { id: item.fsq_id })
      .then((res) => JSON.parse(res))
      .then((res) => {
         if (res[0]) item.img = res[0].prefix + 'original' + res[0].suffix;
      })
      .then(() => {
         clone.querySelector('img').src = item.img;
         clone.querySelector('.coverImage').textContent = item.name;
         clone.querySelector('h3').textContent = item.name;
         clone.querySelector('.desc').textContent = item.categories[0].name;
         clone.querySelector('.stars').textContent = '4.5';
         clone.querySelector('.people').textContent = 'For 2 people';
         clone.querySelector('.subCard > button').id = 'add_' + item.fsq_id;
         const fav = clone.querySelector('.favorite');
         fav.id = 'fav_' + item.fsq_id;
         isChecked(item.fsq_id).then((r) => {
            fav.checked = r;
         });
         clone
            .querySelector('.favorite + label')
            .setAttribute('for', 'fav_' + item.fsq_id);
         if (localStorage.getItem(item.fsq_id))
            clone.querySelector('.subCard > button').textContent = '✅';
         $('.cardContainerFlex').append(clone);
         console.log('Creado');
      })
      .catch((err) => {
         console.log(err);
      });
};

const add = (el) => {
   const id = el.id.split('_')[1];
   el.textContent = el.textContent === 'Add' ? '✅' : 'Add';
   localStorage.removeItem(id);
   if (el.textContent === '✅') localStorage.setItem(id, categorie);
};
