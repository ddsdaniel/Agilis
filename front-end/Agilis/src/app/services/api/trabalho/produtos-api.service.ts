import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StringContainer } from 'src/app/models/string-container';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Tema } from 'src/app/models/trabalho/temas/tema';

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

  adicionarTema(produtoId: string, nome: string): Observable<Tema> {
    return super.post<StringContainer, Tema>({ texto: nome }, `${produtoId}/temas`);
  }

}
