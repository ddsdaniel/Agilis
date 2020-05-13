import { Injectable } from '@angular/core';
import { localStorageKeys } from 'src/assets/constantes/local-storage-keys';

import { UsuarioLogado } from '../models/pessoas/usuario-logado';
import { UsuarioApiService } from './api/pessoas/usuario-api.service';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AppLoadService {

  constructor(
    private usuarioApiService: UsuarioApiService,
    private localStorageService: LocalStorageService,
  ) { }

  initializeApp() {
    this.recuperarUsuarioLogadoDoLocalStorage();
  }

  private recuperarUsuarioLogadoDoLocalStorage() {
    if (this.usuarioApiService.usuarioLogado) {
      return;
    }

    const logado = this.localStorageService.getJson<UsuarioLogado>(localStorageKeys.usuarioLogado);
    this.usuarioApiService.usuarioLogado = logado;

  }

}
