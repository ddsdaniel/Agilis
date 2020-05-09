import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/seguranca/login/login.component';
import { UserStoriesFormComponent } from './components/trabalho/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesHomeComponent } from './components/trabalho/user-stories/user-stories-home/user-stories-home.component';
import { AngularMaterialModule } from './modules/angular-material/angular-material.module';
import { HttpsRequestInterceptorService } from './services/interceptors/http-request-interceptor.service';
import { AppLoadService } from './services/app-load.service';

export function InitApp(appLoadService: AppLoadService) {
  return () => appLoadService.initializeApp();
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserStoriesHomeComponent,
    UserStoriesFormComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    FlexLayoutModule,
    HttpClientModule
  ],
  exports: [
    AngularMaterialModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpsRequestInterceptorService, multi: true, },
    { provide: APP_INITIALIZER, useFactory: InitApp, deps: [AppLoadService], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
