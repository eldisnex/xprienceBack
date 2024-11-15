const likePlace = (e) => {
   $.post('/Home/HandleLikePlace', { idPlan: e.id.split('_')[1] });
};

const isChecked = async (id) => {
   return await $.post('/Home/IsChecked', { idPlan: id }).then((res) => res.isChecked);
};
