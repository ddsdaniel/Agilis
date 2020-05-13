import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/seguranca/login/login.component';
import { TimesFormComponent } from './components/trabalho/times/times-form/times-form.component';
import { TimesComponent } from './components/trabalho/times/times/times.component';
import { ProdutosFormComponent } from './components/trabalho/produtos/produtos-form/produtos-form.component';
import { ProdutosComponent } from './components/trabalho/produtos/produtos/produtos.component';
import { UserStoriesFormComponent } from './components/trabalho/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesComponent } from './components/trabalho/user-stories/user-stories/user-stories.component';
import { AngularMaterialModule } from './modules/angular-material/angular-material.module';
import { AppLoadService } from './services/app-load.service';
import { HttpsRequestInterceptorService } from './services/interceptors/http-request-interceptor.service';
import './prototypes/array-prototypes';

export function InitApp(appLoadService: AppLoadService) {
  return () => appLoadService.initializeApp();
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TimesComponent,
    TimesFormComponent,
    ProdutosComponent,
    ProdutosFormComponent,
    UserStoriesComponent,
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
