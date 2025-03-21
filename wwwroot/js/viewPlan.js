const getName = (item, id) => {
   $.post(handleDetails, { id: id })
      .then((res) => JSON.parse(res))
      .then((res) => {
         item.textContent = res.name;
      });
};

document.querySelectorAll('h3').forEach((e) => {
   getName(e, e.id);
});
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
// map.setView(Object.values({
//    latitude: ,
//    logitude:
// }), 13); Falta saber la ubicacion
// const m = L.marker(Object.values(item.geocodes.main), { icon: customIcon })
// .addTo(map)
// .bindPopup(
//    `
//    <h3>${item.name}</h3>
//    <p>${item.location.address}</p>
//    `
// );