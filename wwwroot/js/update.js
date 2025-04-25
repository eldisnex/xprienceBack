document.getElementById('username').addEventListener('input', function () {
   fetch(
      CHANGE_USERNAME_URL +
         '?' +
         new URLSearchParams({
            userChanged: this.value
         })
   );
});

document.getElementById('mail').addEventListener('input', function () {
   fetch(
      CHANGE_MAIL_URL +
         '?' +
         new URLSearchParams({
            mailChanged: this.value
         })
   );
});
