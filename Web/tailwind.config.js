/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}"],
  theme: {
    extend: {
      width: {
        '1/10' : '10%',
        '2/10' : '20%',
        '3/10' : '30%',
        '4/10' : '40%',
        '5/10' : '50%',
        '6/10' : '60%',
        '7/10' : '70%',
        '8/10' : '80%',
        '9/10' : '90%',
        '5/100': '5%',
        '15/100': '15%',
        '35/100': '35%',
      },
      height: {
        '1/4' : '25vh',
        '5/100': '5vh',
        '1/10' : '10vh',
        '2/10' : '20vh',
        '3/10' : '30vh',
        '4/10' : '40vh',
        '5/10' : '50vh',
        '6/10' : '60vh',
        '7/10' : '70vh',
        '8/10' : '80vh',
        '9/10' : '90vh',
      },
      colors: {
        'gray#F8F8F8': '#F8F8F8',
        'gray#FDFDFD': '#FDFDFD',
        'black#2E383F': '##2E383F',
        'yellow#FDC57B': '#FDC57B',
        'blue#E9F7FA': '#E9F7FA',
        'blue#f0f4fc': '#f0f4fc',
        'navy#07617D': '#07617D',
        'yellow#FDC57B': '#FDC57B'
      }
    },
  },
  plugins: [
    require('tailwind-scrollbar')({ nocompatible: true }),
  ],
  variants: {
      scrollbar: ['rounded']
  }
}
