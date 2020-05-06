import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/seguranca/login/login.component';
import { UserStoriesFormComponent } from './components/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesHomeComponent } from './components/user-stories/user-stories-home/user-stories-home.component';
import { AngularMaterialModule } from './modules/angular-material/angular-material.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserStoriesHomeComponent,
    UserStoriesFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule
  ],
  exports: [
    AngularMaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
