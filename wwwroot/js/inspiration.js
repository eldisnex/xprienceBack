'use strict';

const numbers = [10000, 14000, 17000, 18000, 13000, 16000];
const categories = {
   10000: "Arts and Entertainment",
   13000: "Dining and Drinking",
   14000: "Event",
   16000: "Landmarks and Outdoors",
   17000: "Retail",
   18000: "Sports and Recreation",
}

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

const createCardSection = (num) => {
   options.code = num;
   console.log('options ', options);
   $.post(handleCreateUrl, options)
      .then((res) => JSON.parse(res).results)
      .then((res) => {
         const h4 = document.createElement('h4');
         h4.textContent = categories[num];
         h4.classList.add('rp');
         const iC = document.createElement('section');
         iC.classList.add('inspirationCategories');
         document.querySelector('main').appendChild(h4);
         res.forEach((item) => {
            const template = document.getElementById('cardTemplate').content;
            const clone = document.importNode(template, true);
            $.post(handleImage, { id: item.fsq_id })
               .then((res) => JSON.parse(res))
               .then((res) => {
                  if (res[0])
                     item.img = res[0].prefix + 'original' + res[0].suffix;
               })
               .then(() => {
                  clone.querySelector('img').src = item.img;
                  clone.querySelector('.coverImage').textContent = item.name;
                  clone.querySelector('h3').textContent = item.name;
                  clone.querySelector('.desc').textContent =
                     item.categories[0].name;
                  clone.querySelector('.stars').textContent = '4.5';
                  clone.querySelector('.people').textContent = 'For 2 people';
                  clone.querySelector('.favorite').id = 'fav_' + item.fsq_id;
                  clone
                     .querySelector('.favorite + label')
                     .setAttribute('for', 'fav_' + item.fsq_id);
                  iC.appendChild(clone);
               })
               .catch((err) => {
                  console.log(err);
               });
         });
         document.querySelector('main').append(iC);
      });
};

for (const num of numbers) {
   createCardSection(num);
}
