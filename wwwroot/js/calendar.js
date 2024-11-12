let actualYear = 2024;
let actualMonth = 0;
const selectDay =
   location.href.split('?').length === 2 &&
   location.href.includes('selectDay=True');

const getHtmlFor = (year, month) => {
   const locale = 'es';

   const intlForMonths = new Intl.DateTimeFormat(locale, { month: 'long' });
   const months = [...Array(12).keys()];

   const intlForWeeks = new Intl.DateTimeFormat(locale, { weekday: 'short' });
   const weekDays = [...Array(7).keys()].map((dayIndex) =>
      intlForWeeks.format(new Date(2021, 2, dayIndex + 1))
   );

   const calendar = months.map((monthIndex) => {
      const monthName = intlForMonths.format(new Date(actualYear, monthIndex));
      const nextMonthIndex = (monthIndex + 1) % 12;
      const daysOfMonth = new Date(actualYear, nextMonthIndex, 0).getDate();
      const startsOn = new Date(actualYear, monthIndex, 1).getDay();

      return {
         daysOfMonth,
         monthName,
         startsOn
      };
   });

   console.log(calendar);
   calendarMonth = [calendar[actualMonth]];

   const html = calendarMonth
      .map(({ daysOfMonth, monthName, startsOn }) => {
         const days = [...Array(daysOfMonth).keys()];
         const firstDayAttributes = `class='first-day' style='--first-day-start: ${startsOn}'`;
         const htmlDaysName = weekDays
            .map((dayName) => `<li class='day-name'>${dayName}</li>`)
            .join('');
         const htmlDays = days
            .map(
               (day, index) =>
                  `<li ${index === 0 ? firstDayAttributes : ''} ${
                     selectDay ? 'select="true"' : ''
                  }>${day + 1}</li>`
            )
            .join('');
         return `<div id="${monthName}_${actualYear}"><h1>${monthName} ${actualYear}</h1><ol>${htmlDaysName}${htmlDays}</ol></div>`;
      })
      .join('');

   document.querySelector('.calendar').innerHTML = html;
};

getHtmlFor(actualYear, actualMonth);
let allElements = [];

$('#nm').click(() => {
   actualMonth++;
   if (actualMonth % 12 === 0) {
      actualMonth = 0;
      actualYear++;
   }
   getHtmlFor(actualYear, actualMonth);
   updateDisplayedElements();
});

$('#pm').click(() => {
   actualMonth--;
   if (actualMonth === -1) {
      actualMonth = 11;
      actualYear--;
   }
   getHtmlFor(actualYear, actualMonth);
   updateDisplayedElements();
});

const updateDisplayedElements = () => {
   console.log(actualMonth, actualYear);
   allElements = [];
   document.querySelectorAll('li[select="true"]').forEach((e) => {
      allElements.push(e);
      e.addEventListener('click', () => {
         allElements.forEach((subE) => {
            subE.classList.remove('selected');
         });
         e.classList.add('selected');
      });
   });
};

updateDisplayedElements();

$('.nextButton').click(() => {
   for (var i = 0; i < localStorage.length; i++) {
      if (
         allElements.findIndex((e) => e.classList.contains('selected')) == -1
      ) {
         $('.nextButton').addClass('nextButtonAnim');
         setTimeout(() => {
            $('.nextButton').removeClass('nextButtonAnim');
         }, 300);
         return;
      }
   }
   // Cerrar plan
   $.post(handleCerrar, {
      plan: allStorage().join(';'),
      day: allElements.find((e) => e.classList.contains('selected')).innerText,
      month: actualMonth,
      year: actualYear
   }).done((r) => {
      if (r.created) {
         // Borrar datos
         location.href = endUrl + '?id=' + r.plan.id;
      }
      // Hubo un error
   });
});

function allStorage() {
   var archive = [],
      keys = Object.keys(localStorage),
      i = 0,
      key;

   for (; (key = keys[i]); i++) {
      archive.push(key + '=' + localStorage.getItem(key));
   }
   archive = archive.map((e) => [e.split('=')[0], e.split(',')[1]].join(','));
   return archive;
}
