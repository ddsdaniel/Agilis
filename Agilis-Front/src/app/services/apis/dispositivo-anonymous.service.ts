import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UsuarioLogado } from 'src/app/models/usuario-logado';
import { ApiRestBaseService } from './api-rest-base.service';

@Injectable({
  providedIn: 'root'
})
export class DispositivoAnonymousService extends ApiRestBaseService {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'DispositivoAnonymous');
  }

  excluir(usuarioLogado: UsuarioLogado): Observable<void> {
    return super.delete(`${usuarioLogado.usuario.id}/${usuarioLogado.dispositivo.id}`);
  }
}
