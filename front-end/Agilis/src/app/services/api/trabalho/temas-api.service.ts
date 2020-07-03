import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tema } from 'src/app/models/trabalho/temas/tema';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class TemasApiService extends CrudApiBaseService<Tema> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'temas');
  }

  pesquisarPorProduto(filtro: string, produtoId: string): Observable<Tema[]> {
    return super.get<Tema[]>('pesquisa-crud', this.buildParams({ filtro, produtoId }));
  }
}
