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
