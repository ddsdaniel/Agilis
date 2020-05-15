import { Injectable } from '@angular/core';
import { localStorageKeys } from 'src/assets/constantes/local-storage-keys';

import { UsuarioLogado } from '../models/pessoas/usuario-logado';
import { UsuariosApiService } from './api/pessoas/usuarios-api.service';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AppLoadService {

  constructor(
    private usuariosApiService: UsuariosApiService,
    private localStorageService: LocalStorageService,
  ) { }

  initializeApp() {
    this.recuperarUsuarioLogadoDoLocalStorage();
  }

  private recuperarUsuarioLogadoDoLocalStorage() {
    if (this.usuariosApiService.usuarioLogado) {
      return;
    }

    const logado = this.localStorageService.getJson<UsuarioLogado>(localStorageKeys.usuarioLogado);
    this.usuariosApiService.usuarioLogado = logado;

  }

}
