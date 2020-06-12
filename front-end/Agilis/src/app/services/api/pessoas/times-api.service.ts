import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Time } from 'src/app/models/pessoas/time';

import { CrudApiBaseService } from '../crud-api-base.service';
import { Email } from 'src/app/models/pessoas/email';
import { Observable } from 'rxjs';
import { UsuarioVO } from 'src/app/models/pessoas/usuario-vo';
import { Release } from 'src/app/models/trabalho/releases/release';
import { ReleaseVO } from 'src/app/models/trabalho/releases/release-vo';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutoVO } from 'src/app/models/trabalho/produtos/produto-vo';

@Injectable({
  providedIn: 'root'
})
export class TimesApiService extends CrudApiBaseService<Time> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'times');
  }

  adicionarAdmin(timeId: string, email: Email): Observable<UsuarioVO> {
    return super.post<Email, UsuarioVO>(email, `${timeId}/administradores`);
  }

  excluirAdmin(timeId: string, adminId: string): Observable<void> {
    return super.delete(`${timeId}/administradores/${adminId}`);
  }

  adicionarColaborador(timeId: string, email: Email): Observable<UsuarioVO> {
    return super.post<Email, UsuarioVO>(email, `${timeId}/colaboradores`);
  }

  excluirColaborador(timeId: string, colabId: string): Observable<void> {
    return super.delete(`${timeId}/colaboradores/${colabId}`);
  }

  adicionarProduto(timeId: string, produto: Produto): Observable<ProdutoVO> {
    return super.post<Produto, ProdutoVO>(produto, `${timeId}/produtos`);
  }

  excluirProduto(timeId: string, prodId: string): Observable<void> {
    return super.delete(`${timeId}/produtos/${prodId}`);
  }

  adicionarRelease(timeId: string, release: Release): Observable<ReleaseVO> {
    return super.post<Release, ReleaseVO>(release, `${timeId}/releases`);
  }

  excluirRelease(timeId: string, prodId: string): Observable<void> {
    return super.delete(`${timeId}/releases/${prodId}`);
  }
}
