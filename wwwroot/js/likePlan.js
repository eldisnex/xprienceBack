const likePlace = (e) => {
   $.post('/Home/HandleLikePlace', { idPlan: e.id.split('_')[1] });
};

const isChecked = async (id) => {
   return await $.post('/Home/IsChecked', { idPlan: id }).then(
      (res) => res.isChecked
   );
};

if (!sessionStorage.getItem('light')) sessionStorage.setItem('light', true);

$('#changeTheme').prop('checked', sessionStorage.getItem('light') !== 'false');

function onChange() {
   if ($('#changeTheme').prop('checked')) {
      sessionStorage.setItem('light', true);
      $('body').get(0).style.setProperty('--color1', '#f5f5f5');
      $('body').get(0).style.setProperty('--white', '#fff');
      $('body').get(0).style.setProperty('--black', '#000');
      $('body')
         .get(0)
         .style.setProperty('--shadow-color', 'rgba(100, 100, 111, 0.2)');
   } else {
      sessionStorage.setItem('light', false);
      $('body').get(0).style.setProperty('--color1', '#000');
      $('body').get(0).style.setProperty('--white', '#000');
      $('body').get(0).style.setProperty('--black', '#fff');
      $('body').get(0).style.setProperty('--shadow-color', '#aaa');
   }
   $('body')
      .get(0)
      .style.setProperty('--shadow', 'var(--shadow-color) 0px 7px 29px 0px');
}

onChange();

$('#changeTheme').change(onChange);
