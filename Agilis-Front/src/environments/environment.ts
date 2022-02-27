// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  nomeAplicacao: 'Agilis',
  apiUrl: 'https://localhost:5001/api',
  hubUrl: 'https://localhost:5001',
  materialIconsUrl: 'assets/json/material-icons.json',
  popularIconsUrl: 'assets/json/popular-icons.json',
  firebase: {
    apiKey: 'AIzaSyBBtpX9aJuw5mPH7ZrQ471GYG65R4LhedQ',
    authDomain: 'app-Agilis.firebaseapp.com',
    databaseURL: 'https://app-Agilis.firebaseio.com',
    projectId: 'app-Agilis',
    storageBucket: 'app-Agilis.appspot.com',
    messagingSenderId: '529958958217',
    appId: '1:529958958217:web:1b2dfde9f8a8954acf8258'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
