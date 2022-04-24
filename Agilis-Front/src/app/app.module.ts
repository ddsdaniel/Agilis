import './prototypes/array-prototypes';
import './prototypes/date-prototypes';

import { CommonModule, HashLocationStrategy, LocationStrategy, registerLocaleData } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import localePt from '@angular/common/locales/pt';
import { APP_INITIALIZER, ErrorHandler, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from '@angular/service-worker';

import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AlterarMinhaSenhaComponent } from './components/alterar-minha-senha/alterar-minha-senha.component';
import { BemVindoComponent } from './components/bem-vindo/bem-vindo.component';
import { EsqueciMinhaSenhaComponent } from './components/esqueci-senha/esqueci-minha-senha/esqueci-minha-senha.component';
import {
  RedefinirMinhaSenhaComponent,
} from './components/esqueci-senha/redefinir-minha-senha/redefinir-minha-senha.component';
import { ExcluirMinhaContaComponent } from './components/excluir-minha-conta/excluir-minha-conta.component';
import { IntroComponent } from './components/intro/intro.component';
import { LoginComponent } from './components/login/login.component';
import { NovoUsuarioComponent } from './components/usuarios/novo-usuario/novo-usuario.component';
import { BottomSheetComponent } from './components/widgets/bottom-sheet/bottom-sheet.component';
import { DialogoInputComponent } from './components/widgets/dialogo-input/dialogo-input.component';
import { GlobalErrorHandler } from './handlers/global-error-handler';
import { HttpsRequestInterceptorService } from './interceptors/https-request-interceptor.service';
import { GlobalModule } from './modules/global.module';
import { AppLoadService } from './services/app-load.service';



export function InitApp(appLoadService: AppLoadService) {
  return () => appLoadService.initializeApp();
}

registerLocaleData(localePt, 'pt-BR');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    BemVindoComponent,
    NovoUsuarioComponent,
    BottomSheetComponent,
    ExcluirMinhaContaComponent,
    IntroComponent,
    AlterarMinhaSenhaComponent,
    EsqueciMinhaSenhaComponent,
    RedefinirMinhaSenhaComponent,
    DialogoInputComponent,
  ],
  imports: [
    GlobalModule,
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    AppRoutingModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      // Register the ServiceWorker as soon as the app is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    }),
  ],
  entryComponents: [
    BottomSheetComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpsRequestInterceptorService, multi: true, },
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
    { provide: APP_INITIALIZER, useFactory: InitApp, deps: [AppLoadService], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
