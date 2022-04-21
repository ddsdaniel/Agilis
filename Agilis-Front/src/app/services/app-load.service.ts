import { Injectable } from '@angular/core';
import { AutenticacaoService } from './autenticacao.service';


@Injectable({
  providedIn: 'root'
})
export class AppLoadService {

  constructor(
    private autenticacaoService: AutenticacaoService,
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
