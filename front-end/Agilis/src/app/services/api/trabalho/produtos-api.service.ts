import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Sprint } from 'src/app/models/trabalho/sprints/sprint';
import { SprintVO } from 'src/app/models/trabalho/sprints/sprint-vo';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class ProdutosApiService extends CrudApiBaseService<Produto> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'produtos');
  }

  adicionarSprint(produtoId: string, sprint: Sprint): Observable<SprintVO> {
    return super.post<Sprint, SprintVO>(sprint, `${produtoId}/sprints`);
  }

  excluirSprint(produtoId: string, sprintId: string): Observable<void> {
    return super.delete(`${produtoId}/sprints/${sprintId}`);
  }

}
