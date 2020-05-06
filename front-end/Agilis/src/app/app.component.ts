import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  myWorkRoutes = [

    {
      icon: 'assignment',
      route: 'sales/activities',
      title: 'ACTIVITIES'
    },
    {
      icon: 'dashboard',
      route: 'sales/dashboards',
      title: 'DASHBOARDS'
    }
  ];
}
