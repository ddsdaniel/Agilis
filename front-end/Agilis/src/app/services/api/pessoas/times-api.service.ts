import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Email } from 'src/app/models/pessoas/email';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioFK } from 'src/app/models/pessoas/usuario-FK';
import { StringContainer } from 'src/app/models/string-container';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutoFK } from 'src/app/models/trabalho/produtos/produto-fk';
import { ReleaseFK } from 'src/app/models/trabalho/releases/release-fk';
import { SprintFK } from 'src/app/models/trabalho/sprints/sprint-fk';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class TimesApiService extends CrudApiBaseService<Time> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'times');
  }

  adicionarAdmin(timeId: string, email: Email): Observable<UsuarioFK> {
    return super.post<Email, UsuarioFK>(email, `${timeId}/administradores`);
  }

  excluirAdmin(timeId: string, adminId: string): Observable<void> {
    return super.delete(`${timeId}/administradores/${adminId}`);
  }

  adicionarColaborador(timeId: string, email: Email): Observable<UsuarioFK> {
    return super.post<Email, UsuarioFK>(email, `${timeId}/colaboradores`);
  }

  excluirColaborador(timeId: string, colabId: string): Observable<void> {
    return super.delete(`${timeId}/colaboradores/${colabId}`);
  }

  adicionarProduto(timeId: string, nome: string): Observable<Produto> {
    return super.post<StringContainer, ProdutoFK>({ texto: nome }, `${timeId}/produtos`);
  }

  excluirProduto(timeId: string, prodId: string): Observable<void> {
    return super.delete(`${timeId}/produtos/${prodId}`);
  }

  adicionarRelease(timeId: string, nome: string): Observable<ReleaseFK> {
    return super.post<StringContainer, ReleaseFK>({ texto: nome }, `${timeId}/releases`);
  }

  excluirRelease(timeId: string, prodId: string): Observable<void> {
    return super.delete(`${timeId}/releases/${prodId}`);
  }

  renomear(id: string, nome: string): Observable<void> {
    return super.patch<StringContainer, void>({ texto: nome }, `${id}`);
  }

}
