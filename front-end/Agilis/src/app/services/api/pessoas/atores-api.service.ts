import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ator } from 'src/app/models/pessoas/ator';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class AtoresApiService extends CrudApiBaseService<Ator> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'atores');
  }

  pesquisarPorProduto(filtro: string, produtoId: string): Observable<Ator[]> {
    return super.get<Ator[]>('pesquisa-crud', this.buildParams({ filtro, produtoId }));
  }
}
