importScripts('https://www.gstatic.com/firebasejs/8.6.7/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/8.6.7/firebase-messaging.js');

self.onnotificationclick = function(event) {
  event.notification.close();

  // This looks to see if the current is already open and
  // focuses if it is
  event.waitUntil(clients.matchAll({
    type: "window"
  }).then(function(clientList) {
    for (var i = 0; i < clientList.length; i++) {
      var client = clientList[i];
      if (client.url == '/' && 'focus' in client)
        return client.focus();
    }
    if (clients.openWindow)
      return clients.openWindow(event.notification.data.FCM_MSG.notification.click_action);
  }));
};

firebase.initializeApp({
  apiKey: "AIzaSyBBtpX9aJuw5mPH7ZrQ471GYG65R4LhedQ",
  authDomain: "app-Agilis.firebaseapp.com",
  databaseURL: "https://app-Agilis.firebaseio.com",
  projectId: "app-Agilis",
  storageBucket: "app-Agilis.appspot.com",
  messagingSenderId: "529958958217",
  appId: "1:529958958217:web:1b2dfde9f8a8954acf8258"
});

const messaging = firebase.messaging();

