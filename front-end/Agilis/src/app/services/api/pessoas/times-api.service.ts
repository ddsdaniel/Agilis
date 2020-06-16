import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Email } from 'src/app/models/pessoas/email';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioVO } from 'src/app/models/pessoas/usuario-vo';
import { Produto } from 'src/app/models/trabalho/produto';
import { Release } from 'src/app/models/trabalho/releases/release';
import { ReleaseFK } from 'src/app/models/trabalho/releases/release-fk';
import { Sprint } from 'src/app/models/trabalho/sprints/sprint';
import { SprintFK } from 'src/app/models/trabalho/sprints/sprint-fk';

import { CrudApiBaseService } from '../crud-api-base.service';

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

  adicionarProduto(timeId: string, produto: Produto): Observable<Produto> {
    return super.post<Produto, Produto>(produto, `${timeId}/produtos`);
  }

  excluirProduto(timeId: string, prodId: string): Observable<void> {
    return super.delete(`${timeId}/produtos/${prodId}`);
  }

  adicionarRelease(timeId: string, release: Release): Observable<ReleaseFK> {
    return super.post<Release, ReleaseFK>(release, `${timeId}/releases`);
  }

  excluirRelease(timeId: string, prodId: string): Observable<void> {
    return super.delete(`${timeId}/releases/${prodId}`);
  }

  adicionarSprint(produtoId: string, sprint: Sprint): Observable<SprintFK> {
    return super.post<Sprint, SprintFK>(sprint, `${produtoId}/sprints`);
  }

  excluirSprint(produtoId: string, sprintId: string): Observable<void> {
    return super.delete(`${produtoId}/sprints/${sprintId}`);
  }
}
