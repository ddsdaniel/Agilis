import { Injectable } from '@angular/core';
import { localStorageKeys } from '../consts/local-storage-keys';
import { UsuarioLogado } from '../models/usuario-logado';
import { AutenticacaoService } from './autenticacao.service';
import { LocalStorageService } from './local-storage.service';


@Injectable({
  providedIn: 'root'
})
export class AppLoadService {

  constructor(
    private autenticacaoService: AutenticacaoService,
    private localStorageService: LocalStorageService,
  ) { }

  initializeApp() {
    this.recuperarUsuarioLogadoDoLocalStorage();
  }

  private recuperarUsuarioLogadoDoLocalStorage() {
    if (this.autenticacaoService.estaLogado()) {
      return;
    }

    this.autenticacaoService.recuperarUsuarioLogadoDoLocalStorage();
  }

}
