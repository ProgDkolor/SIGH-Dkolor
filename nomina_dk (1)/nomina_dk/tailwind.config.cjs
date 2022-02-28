const config = {
  content: ["./src/**/*.{html,js,svelte,ts}"],

  theme: {
    extend: {
      colors: {
        navy: {
          '50':  '#f3f8f9',
          '100': '#daf1fa',
          '200': '#afe0f5',
          '300': '#7cc2e7',
          '400': '#479ed3',
          '500': '#357dc0',
          '600': '#2d62a9',
          '700': '#254a87',
          '800': '#1b3260',
          '900': '#101f3f',
        },
        blue: {
          '50':  '#f6f9fb',
          '100': '#e3f0fd',
          '200': '#c5d9fa',
          '300': '#9db5f3',
          '400': '#798eea',
          '500': '#6269e3',
          '600': '#514dd6',
          '700': '#3e39b6',
          '800': '#2b2788',
          '900': '#181956',
        },
        indigo: {
          '50':  '#f9fafb',
          '100': '#edeffb',
          '200': '#dad4f8',
          '300': '#baaeef',
          '400': '#a485e3',
          '500': '#8c60da',
          '600': '#7344c9',
          '700': '#5733a6',
          '800': '#3c2377',
          '900': '#221748',
        },
        cerise: {
          '50':  '#fdfcfb',
          '100': '#faeff2',
          '200': '#f6cbe6',
          '300': '#ec9ecb',
          '400': '#ea6fac',
          '500': '#df4b91',
          '600': '#c93270',
          '700': '#a22652',
          '800': '#771b35',
          '900': '#49111c',
        },
        cerise: {
          '50':  '#fdfcfa',
          '100': '#fbf0ed',
          '200': '#f8cfda',
          '300': '#eea1b5',
          '400': '#eb718d',
          '500': '#de4e6b',
          '600': '#c7344c',
          '700': '#a02738',
          '800': '#741b25',
          '900': '#471114',
        },
        cocoa: {
          '50':  '#fcfbf8',
          '100': '#faf0d9',
          '200': '#f5d7b1',
          '300': '#e7ae7d',
          '400': '#da804f',
          '500': '#c55d30',
          '600': '#a8431f',
          '700': '#823219',
          '800': '#5a2313',
          '900': '#39150c',
        },
        gold: {
          '50':  '#fbfaf4',
          '100': '#f9f0bd',
          '200': '#f1dd81',
          '300': '#dcb84f',
          '400': '#bd8e2a',
          '500': '#9e6f15',
          '600': '#80550d',
          '700': '#62400c',
          '800': '#432c0b',
          '900': '#2c1b08',
        },
        lemon: {
          '50':  '#faf9f3',
          '100': '#f8f0b5',
          '200': '#eedf76',
          '300': '#d4bd46',
          '400': '#ac9424',
          '500': '#897611',
          '600': '#6e5d0b',
          '700': '#54450a',
          '800': '#392f09',
          '900': '#271d08',
        },
        green: {
          '50':  '#f2f6f3',
          '100': '#dff0e6',
          '200': '#b5e6c5',
          '300': '#7acb94',
          '400': '#35ab62',
          '500': '#25903c',
          '600': '#1f7a2b',
          '700': '#1d5d24',
          '800': '#15401d',
          '900': '#0f2717',
        },
        submarine: {
          '50':  '#eff6f5',
          '100': '#d0f0f4',
          '200': '#9be6e6',
          '300': '#61cbc8',
          '400': '#27aca2',
          '500': '#1c907c',
          '600': '#197963',
          '700': '#175d4d',
          '800': '#123f39',
          '900': '#0c272a',
        },
      }
    },
  },

  plugins: [
    require('daisyui'),
  ],
  daisyui: {
    // themes: [
    //   {
    //     'oficial': {                          /* your theme name */
    //        'primary' : '#444b55',           /* Primary color */
    //        'primary-focus' : '#8462f4',     /* Primary color - focused */
    //        'primary-content' : '#ffffff',   /* Foreground content color to use on primary color */

    //        'secondary' : '#f6d860',         /* Secondary color */
    //        'secondary-focus' : '#f3cc30',   /* Secondary color - focused */
    //        'secondary-content' : '#ffffff', /* Foreground content color to use on secondary color */

    //        'accent' : '#37cdbe',            /* Accent color */
    //        'accent-focus' : '#2aa79b',      /* Accent color - focused */
    //        'accent-content' : '#ffffff',    /* Foreground content color to use on accent color */

    //        'neutral' : '#3d4451',           /* Neutral color */
    //        'neutral-focus' : '#2a2e37',     /* Neutral color - focused */
    //        'neutral-content' : '#ffffff',   /* Foreground content color to use on neutral color */

    //        'base-100' : '#ffffff',          /* Base color of page, used for blank backgrounds */
    //        'base-200' : '#f9fafb',          /* Base color, a little darker */
    //        'base-300' : '#d1d5db',          /* Base color, even more darker */
    //        'base-content' : '#1f2937',      /* Foreground content color to use on base color */

    //        'info' : '#2094f3',              /* Info */
    //        'success' : '#009485',           /* Success */
    //        'warning' : '#ff9900',           /* Warning */
    //        'error' : '#ff5724',             /* Error */
    //     },
    //   },
    // ],
  },

};

module.exports = config;
