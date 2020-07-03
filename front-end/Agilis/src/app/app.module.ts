import './prototypes/array-prototypes';

import { DragDropModule } from '@angular/cdk/drag-drop';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DialogoEmailComponent } from './components/dialogos/dialogo-email/dialogo-email.component';
import { DialogoTextoComponent } from './components/dialogos/dialogo-texto/dialogo-texto.component';
import { TimesFormComponent } from './components/pessoas/times/times-form/times-form.component';
import { TimesComponent } from './components/pessoas/times/times/times.component';
import { TemasFormComponent } from './components/trabalho/temas/temas-form/temas-form.component';
import { TemasComponent } from './components/trabalho/temas/temas/temas.component';
import { EpicosFormComponent } from './components/trabalho/epicos/epicos-form/epicos-form.component';
import { EpicosComponent } from './components/trabalho/epicos/epicos/epicos.component';
import { ProdutosFormComponent } from './components/trabalho/produtos/produtos-form/produtos-form.component';
import { ProdutosComponent } from './components/trabalho/produtos/produtos/produtos.component';
import { UsuariosFormComponent } from './components/pessoas/usuarios/usuarios-form/usuarios-form.component';
import { LoginComponent } from './components/seguranca/login/login.component';
import { ReleasesFormComponent } from './components/trabalho/releases/releases-form/releases-form.component';
import { SprintsFormComponent } from './components/trabalho/sprints/sprints-form/sprints-form.component';
import { UserStoriesFormComponent } from './components/trabalho/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesComponent } from './components/trabalho/user-stories/user-stories/user-stories.component';
import { AngularMaterialModule } from './modules/angular-material/angular-material.module';
import { AppLoadService } from './services/app-load.service';
import { HttpsRequestInterceptorService } from './services/interceptors/http-request-interceptor.service';

export function InitApp(appLoadService: AppLoadService) {
  return () => appLoadService.initializeApp();
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TimesComponent,
    TimesFormComponent,
    TemasComponent,
    TemasFormComponent,
    EpicosComponent,
    EpicosFormComponent,
    ProdutosComponent,
    ProdutosFormComponent,
    ReleasesFormComponent,
    SprintsFormComponent,
    UserStoriesComponent,
    UserStoriesFormComponent,
    UsuariosFormComponent,
    DialogoEmailComponent,
    DialogoTextoComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    FlexLayoutModule,
    HttpClientModule,
    DragDropModule
  ],
  entryComponents: [
    DialogoEmailComponent,
    DialogoTextoComponent
  ],
  exports: [
    AngularMaterialModule,
    DialogoEmailComponent,
    DialogoTextoComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpsRequestInterceptorService, multi: true, },
    { provide: APP_INITIALIZER, useFactory: InitApp, deps: [AppLoadService], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
