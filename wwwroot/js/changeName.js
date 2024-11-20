const input = $('.folder input[type="text"]');
input.on('click', (e) => e.preventDefault());

$('.edit').on('click', (e) => {
   e.preventDefault();
   input.prop('disabled', false);
   input.focus();
});

input.on('blur', (e) => {
   $.post(handleChangeNameUrl, {
      id: input.attr('id').split('_')[1],
      name: input.val()
   });
});
