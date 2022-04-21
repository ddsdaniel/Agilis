import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { tap } from 'rxjs/operators';
import { localStorageKeys } from '../consts/local-storage-keys';
import { Login } from '../models/seguranca/login';
import { UsuarioLogado } from '../models/seguranca/usuario-logado';
import { RefreshTokenApiService } from './apis/refresh-token-api.service';
import { UsuarioApiService } from './apis/usuario-api.service';
import { LocalStorageService } from './local-storage.service';


@Injectable({
  providedIn: 'root'
})
export class AutenticacaoService {

  private login = new BehaviorSubject<UsuarioLogado>(null);
  public onLogin = this.login.asObservable();

  private logOff = new BehaviorSubject<UsuarioLogado>(null);
  public onLogOff = this.logOff.asObservable();

  usuarioLogado: UsuarioLogado;

  constructor(
    private usuarioApiService: UsuarioApiService,
    private localStorageService: LocalStorageService,
    private refreshTokenApiService: RefreshTokenApiService,
  ) { }

  autenticar(login: Login): Observable<UsuarioLogado> {
    return this.usuarioApiService.autenticar(login)
      .pipe(
        tap((logado: UsuarioLogado) => {
          this.persistirUsuarioLogado(logado);
        })
      );
  }

  persistirUsuarioLogado(usuarioLogado: UsuarioLogado) {
    this.usuarioLogado = usuarioLogado;
    this.localStorageService.setJSON<UsuarioLogado>(localStorageKeys.usuarioLogado, usuarioLogado);
    this.login.next(usuarioLogado);
  }

  renovarToken(): Observable<UsuarioLogado> {
    return this.refreshTokenApiService.renovar({
      token: this.usuarioLogado.refreshToken
    })
      .pipe(
        tap((logado: UsuarioLogado) => {
          this.usuarioLogado = logado;
          this.localStorageService.setJSON<UsuarioLogado>(localStorageKeys.usuarioLogado, logado);
          // TODO: this.login.next(logado);
        })
      );
  }

  recuperarUsuarioLogadoDoLocalStorage() {
    const logado = this.localStorageService.getJson<UsuarioLogado>(localStorageKeys.usuarioLogado);
    this.usuarioLogado = logado;
    this.login.next(logado);
  }

  limparUsuarioLogado() {
    const clone = Object.assign({}, this.usuarioLogado);
    this.logOff.next(clone);
    this.usuarioLogado = null;
    this.localStorageService.removeItem(localStorageKeys.usuarioLogado);
  }

  estaLogado(): boolean {
    return this.usuarioLogado !== null && this.usuarioLogado !== undefined;
  }

}
