import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/messaging';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs/internal/Subscription';
import { environment } from 'src/environments/environment';
import { constantes } from './consts/constantes';
import { Dispositivo } from './models/dispositivo';
import { UsuarioLogado } from './models/usuario-logado';
import { DispositivoAnonymousService } from './services/apis/dispositivo-anonymous.service';
import { DispositivoApiService } from './services/apis/dispositivo-api.service';
import { AutenticacaoService } from './services/autenticacao.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  title = 'Agilis';
  private subscriptions = new Subscription();

  constructor(
    private autenticacaoService: AutenticacaoService,
    private afMessaging: AngularFireMessaging,
    private dispositivoApiService: DispositivoApiService,
    private snackBar: MatSnackBar,
    private dispositivoAnonymousService: DispositivoAnonymousService,
  ) { }

  ngOnInit(): void {

    if (environment.production) {
      if (location.protocol === 'http:') {
        window.location.href = location.href.replace('http', 'https');
      }
    }

    this.subscriptions
      .add(this.autenticacaoService.onLogin.subscribe(usuarioLogado => this.escutarNotificacoes(usuarioLogado)))
      .add(this.autenticacaoService.onLogOff.subscribe(usuarioLogado => this.interromperNotificacoes(usuarioLogado)));
  }

  interromperNotificacoes(usuarioLogado: UsuarioLogado): void {

    if (usuarioLogado && usuarioLogado.dispositivo) {
      this.afMessaging.deleteToken(usuarioLogado.dispositivo.token)
        .subscribe({
          next: () => this.excluirDispositivo(usuarioLogado),
          error: (error) => this.snackBar.open(error.message),
        });

    }
  }

  excluirDispositivo(usuarioLogado: UsuarioLogado): void {
    this.dispositivoAnonymousService.excluir(usuarioLogado)
      .subscribe({
        error: (error) => this.snackBar.open(error.message),
      });
  }

  escutarNotificacoes(usuarioLogado: UsuarioLogado): void {
    if (usuarioLogado && !usuarioLogado.dispositivo) {
      this.afMessaging.requestToken
        .subscribe({
          next: (token) => this.adicionarDispositivo(token),
          error: (error) => this.snackBar.open(error.message),
        });
    }
  }

  adicionarDispositivo(token: string): void {

    const dispositivo: Dispositivo = {
      id: constantes.newGuid,
      token
    };

    this.dispositivoApiService.adicionar(dispositivo)
      .subscribe({
        next: id => this.autenticacaoService.usuarioLogado.dispositivo = { id, token },
        error: (error: HttpErrorResponse) => this.snackBar.open(error.message)
      });
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

}
