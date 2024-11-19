// ---- Map ----
const map = L.map('map');

L.tileLayer('https://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}{r}.png', {
   maxZoom: 19,
   attribution: '© Xprience'
}).addTo(map);

const customIcon = L.icon({
   iconUrl: '../img/svg/mapPin.svg',
   iconSize: [32, 32],
   iconAnchor: [16, 0]
});

for (var i = 0; i < localStorage.length; i++) {
   const key = localStorage.key(i);
   $.post(handleDetails, { id: key })
      .then((res) => JSON.parse(res))
      .then((res) => {
         createLine(res);
      });
}

const markers = [];

const createLine = (item) => {
   markers.push(item);
   const m = L.marker(Object.values(item.geocodes.main), { icon: customIcon })
      .addTo(map)
      .bindPopup(
         `
         <h3>${item.name}</h3>
         <p>${item.location.address}</p>
         `
      );
   if (markers.length === 1) {
      map.setView(Object.values(markers[0].geocodes.main), 13);
      m.openPopup();
   }
   console.log(item);
   const template = document.getElementById('itemTemplate').content;
   const clone = document.importNode(template, true);
   clone.querySelector('h3').textContent = item.name;
   const timeInput = clone.querySelector('.receiveTime');
   timeInput.id = 'time_' + item.fsq_id;
   const DELETE = clone.querySelector('.delete');
   DELETE.id = 'delete_' + item.fsq_id;
   DELETE.addEventListener('click', (e) => {
      localStorage.removeItem(item.fsq_id);
      location.reload();
   });
   const info = localStorage.getItem(item.fsq_id);
   const arrInfo = info.split(',');
   if (arrInfo.length === 2) {
      timeInput.value = arrInfo[1];
   }
   $('.items').append(clone);
};

const handleChangeTime = (item) => {
   id = item.id.split('_')[1];
   localStorage.setItem(id, localStorage.getItem(id).split(',')[0]);
   localStorage.setItem(id, [localStorage.getItem(id), item.value].join(','));
};

$('.nextButton').click(() => {
   for (var i = 0; i < localStorage.length; i++) {
      if (localStorage.getItem(localStorage.key(i)).split(',').length !== 2) {
         $('.nextButton').addClass('nextButtonAnim');
         setTimeout(() => {
            $('.nextButton').removeClass('nextButtonAnim');
         }, 300);
         return;
      }
   }
   location.href = calendar + `?selectDay=True`;
});
