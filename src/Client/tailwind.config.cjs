/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ['./src/**/*.{html,js,svelte,ts}'],
	theme: {
		extend: {
			colors: {
				bg: '#202433',
				bgsecondary: '#281A1C',
				primary: '#FC728B',
				secondary: '#F4C395',
				green: {
					300: '#9CFEFE'
				},
				purple: {
					400: '#BF66EB',
					500: '#E3428F'
				},
				gray: {
					500: '#33394F'
				},
				pink: {
					300: '#FFE3CA',
					400: '#F17272'
				},
				blue: {
					400: '#5960FF'
				},
				yellow: {
					400: '#FBFF34'
				}
			}
		}
	},
	plugins: []
};
