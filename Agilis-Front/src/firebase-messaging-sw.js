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
  apiKey: "AIzaSyBwWdIarzt-D7jtLZ995LW36QXvAN9hCJM",
  authDomain: "agilis-f7c41.firebaseapp.com",
  projectId: "agilis-f7c41",
  storageBucket: "agilis-f7c41.appspot.com",
  messagingSenderId: "448778142805",
  appId: "1:448778142805:web:eb79969fa49c78471440dc"
});

const messaging = firebase.messaging();

