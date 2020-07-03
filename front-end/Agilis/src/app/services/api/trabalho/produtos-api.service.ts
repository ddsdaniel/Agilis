import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/trabalho/produtos/produto';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class ProdutosApiService extends CrudApiBaseService<Produto> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'produtos');
  }

  pesquisarPorTime(filtro: string, timeId: string): Observable<Produto[]> {
    return super.get<Produto[]>('pesquisa-crud', this.buildParams({ filtro, timeId }));
  }
}
