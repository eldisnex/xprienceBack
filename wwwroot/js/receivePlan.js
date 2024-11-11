for (var i = 0; i < localStorage.length; i++) {
   const key = localStorage.key(i);
   $.post(handleDetails, { id: key })
      .then((res) => JSON.parse(res))
      .then((res) => {
         createLine(res);
      });
}

const createLine = (item) => {
   console.log(item);
   const template = document.getElementById('itemTemplate').content;
   const clone = document.importNode(template, true);
   clone.querySelector('h3').textContent = item.name;
   const timeInput = clone.querySelector('.receiveTime');
   timeInput.id = 'time_' + item.fsq_id;
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
