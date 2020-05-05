import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { UserStoriesFormComponent } from './components/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesHomeComponent } from './components/user-stories/user-stories-home/user-stories-home.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserStoriesHomeComponent,
    UserStoriesFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
