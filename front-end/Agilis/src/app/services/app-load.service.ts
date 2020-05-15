import { Injectable } from '@angular/core';
import { localStorageKeys } from 'src/assets/constantes/local-storage-keys';

import { UsuarioLogado } from '../models/pessoas/usuario-logado';
import { LocalStorageService } from './local-storage.service';
import { AutenticacaoService } from './seguranca/autenticacao.service';

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
    if (this.autenticacaoService.usuarioLogado) {
      return;
    }

    const logado = this.localStorageService.getJson<UsuarioLogado>(localStorageKeys.usuarioLogado);
    this.autenticacaoService.usuarioLogado = logado;

  }

}
