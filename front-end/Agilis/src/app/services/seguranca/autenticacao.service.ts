import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { UsuarioLogado } from 'src/app/models/pessoas/usuario-logado';
import { LoginDados } from 'src/app/models/seguranca/login-dados';
import { localStorageKeys } from 'src/assets/constantes/local-storage-keys';

import { UsuariosApiService } from '../api/pessoas/usuarios-api.service';
import { LocalStorageService } from '../local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoService {

  usuarioLogado: UsuarioLogado;

  constructor(
    private usuariosApiService: UsuariosApiService,
    private localStorageService: LocalStorageService,
  ) { }

  autenticar(login: LoginDados): Observable<UsuarioLogado> {
    return this.usuariosApiService.autenticar(login)
      .pipe(
        tap((logado: UsuarioLogado) => {
          this.usuarioLogado = logado;
          this.localStorageService.setJSON<UsuarioLogado>(localStorageKeys.usuarioLogado, logado);
        })
      );
  }

  limparUsuarioLogado() {
    this.usuarioLogado = null;
    this.localStorageService.removeItem(localStorageKeys.usuarioLogado);
  }

}
